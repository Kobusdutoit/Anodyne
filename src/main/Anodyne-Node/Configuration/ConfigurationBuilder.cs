// Copyright 2011-2013 Anodyne.
//   
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
//  
//      http://www.apache.org/licenses/LICENSE-2.0 
//  
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.

using Kostassoid.Anodyne.Domain.DataAccess;
using Kostassoid.Anodyne.Domain.DataAccess.Policy;
using Kostassoid.Anodyne.Node.Dependency.Registration;

namespace Kostassoid.Anodyne.Node.Configuration
{
    using System.Reflection;
    using Anodyne.DataAccess;
    using Common.Tools;
    using Dependency;
    using Logging;
    using Subsystem;
    using Wcf;
    using System;

    internal class ConfigurationBuilder : INodeConfigurator, INodeConfiguratorEx
    {
        private readonly NodeConfiguration _configuration;

        //INodeConfiguration INodeConfiguratorEx.Configuration { get { return _configuration; } }
        public INodeConfiguration Configuration { get { return _configuration; } }

        public ConfigurationBuilder()
        {
            _configuration = new NodeConfiguration
                {
                    RuntimeMode = RuntimeMode.Production,
                    SystemNamespace = DetectSystemNamespace(),
                    LoggerAdapter = new NullLoggerAdapter()
                };
        }

        private static string DetectSystemNamespace()
        {
            var assembly = Assembly.GetEntryAssembly(); // could be null during tests run
            return assembly != null ? assembly.FullName.Substring(0, assembly.FullName.IndexOfAny(new[] { '.', '-', '_' })) : "";
        }

        void INodeConfiguratorEx.SetContainerAdapter(IContainer container)
        {
            if (_configuration.Container != null)
                throw new InvalidOperationException("Container adapter is already set to " + _configuration.Container.GetType().Name);

            _configuration.Container = container;
        }

        void INodeConfiguratorEx.SetLoggerAdapter(ILoggerAdapter loggerAdapter)
        {
            _configuration.LoggerAdapter = loggerAdapter;
            LogManager.Adapter = loggerAdapter; //TODO: move
        }

        void INodeConfiguratorEx.SetWcfProxyFactory(IWcfProxyFactory wcfProxyFactory)
        {
            _configuration.WcfProxyFactory = wcfProxyFactory;
        }

        void INodeConfiguratorEx.SetDataAccessProvider(IDataAccessProvider dataAccessProvider)
        {
            _configuration.DataAccess = dataAccessProvider;
        }

        public void RunIn(RuntimeMode runtimeMode)
        {
            _configuration.RuntimeMode = runtimeMode;
        }

        public void DefineSystemNamespaceAs(string systemNamespace)
        {
            _configuration.SystemNamespace = systemNamespace;
        }

        public void UseDataAccessPolicy(Action<DataAccessPolicy> policyAction)
        {
            var dataPolicy = new DataAccessPolicy();
            policyAction(dataPolicy);

            UnitOfWork.EnforcePolicy(dataPolicy);
        }

        private bool CanContinue(ConfigurationPredicate when)
        {
            return when == null || when(_configuration);
        }

        public void ConfigureUsing<TConfiguration>(ConfigurationPredicate when) where TConfiguration : IConfigurationAction
        {
            if (!CanContinue(when)) return;
            Activator.CreateInstance<TConfiguration>().OnConfigure(_configuration);
        }

        public void ConfigureUsing(Action<INodeConfiguration> configurationAction, ConfigurationPredicate when)
        {
            if (!CanContinue(when)) return;
            configurationAction(_configuration);
        }

        private static string GetTypeUniqueName<T>(string prefix)
        {
            return prefix + "-" + typeof (T).Name;
        }

        public void OnStartupPerform<TStartup>(ConfigurationPredicate when) where TStartup : IStartupAction
        {
            if (!CanContinue(when)) return;
            EnsureContainerIsSet();

            _configuration.Container.Put(Binding.For<IStartupAction>().Use<TStartup>().With(Lifestyle.Singleton).Named(GetTypeUniqueName<TStartup>("Startup")));
        }

        public void OnStartupPerform(Action<INodeConfiguration> startupAction, ConfigurationPredicate when)
        {
            if (!CanContinue(when)) return;
            EnsureContainerIsSet();

            _configuration.Container.Put(Binding.For<IStartupAction>().Use(() => new StartupActionWrapper(startupAction)).With(Lifestyle.Singleton).Named("Startup-" + SeqGuid.NewGuid()));
        }

        public void OnShutdownPerform<TShutdown>(ConfigurationPredicate when) where TShutdown : IShutdownAction
        {
            if (!CanContinue(when)) return;
            EnsureContainerIsSet();

            _configuration.Container.Put(Binding.For<IShutdownAction>().Use<TShutdown>().With(Lifestyle.Singleton).Named(GetTypeUniqueName<TShutdown>("Shutdown")));
        }

        public void OnShutdownPerform(Action<INodeConfiguration> shutdownAction, ConfigurationPredicate when)
        {
            if (!CanContinue(when)) return;
            _configuration.Container.Put(Binding.For<IShutdownAction>().Use(() => new ShutdownActionWrapper(shutdownAction)).With(Lifestyle.Singleton).Named("Shutdown-" + SeqGuid.NewGuid()));
        }

        public void RegisterSubsystem<TSubsystem>() where TSubsystem : ISubsystem
        {
            _configuration.Container.Put(Binding.For<ISubsystem>().Use<TSubsystem>().With(Lifestyle.Singleton));
        }

        private void EnsureConfigurationIsValid()
        {
            EnsureContainerIsSet();
        }

        private void EnsureContainerIsSet()
        {
            if (_configuration.Container == null)
                throw new InvalidOperationException("Node container should be configured first");
        }

        public INodeConfiguration Build()
        {
            EnsureConfigurationIsValid();

            return _configuration;
        }
    }
}