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

namespace Kostassoid.Anodyne.Abstractions.Wcf
{
    using Registration;
    using Registration.Internal;

    /// <summary>
    /// Base Wcf proxy factory.
    /// </summary>
    public abstract class WcfProxyFactory : IWcfProxyFactory
    {
        /// <summary>
        /// Register and start service as a Wcf service.
        /// </summary>
        /// <typeparam name="TService">Service interface.</typeparam>
        /// <returns>Service implementation syntax.</returns>
        public IServiceImplementationSyntax<TService> Start<TService>() where TService : class
        {
            return new ServiceImplementationSyntax<TService>(this);
        }

        /// <summary>
        /// Set up Wcf client proxy.
        /// </summary>
        /// <typeparam name="TService">Service interface.</typeparam>
        /// <param name="endpoint">Wcf endpoint specification.</param>
        public abstract void Consume<TService>(WcfEndpointSpecification endpoint) where TService : class;

        /// <summary>
        /// Publish Wcf service.
        /// </summary>
        /// <typeparam name="TService">Service interface.</typeparam>
        /// <typeparam name="TImpl">Service implementation.</typeparam>
        /// <param name="specification">Wcf publishing specification.</param>
        public abstract void Publish<TService, TImpl>(WcfServiceSpecification<TService, TImpl> specification)
            where TService : class
            where TImpl : TService;
    }
}