﻿// Copyright 2011-2013 Anodyne.
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
using Kostassoid.Anodyne.Node.Dependency.Registration;

namespace Kostassoid.Anodyne.MongoDb
{
    using DataAccess;
    using Node.Configuration;
    using Node.DataAccess;
    using System;

    public static class ConfigurationEx
    {
        public static void UseMongoDataAccess(this INodeConfigurator nodeConfigurator, string databaseServer, string databaseName)
        {
            var cfg = nodeConfigurator.Configuration;

            cfg.Container.Put(Binding.For<IDataAccessProvider>().UseInstance(new MongoDataAccessProvider(cfg.SystemNamespace, databaseServer, databaseName)));
            cfg.Container.Put(Binding.For<IDataAccessContext>().Use<DefaultDataAccessContext>());

            ((INodeConfiguratorEx)nodeConfigurator).SetDataAccessProvider(cfg.Container.Get<IDataAccessProvider>());

            UnitOfWork.SetDependencyResolvers(cfg.Container.Get<IDataAccessProvider>().SessionFactory, new ContainerOperationResolver(cfg.Container), new MongoRepositoryResolver());
        }

        public static void UseMongoDataAccess(this INodeConfigurator nodeConfigurator, Tuple<string, string> databaseServerAndName)
        {
            UseMongoDataAccess(nodeConfigurator, databaseServerAndName.Item1, databaseServerAndName.Item2);
        }
    }
}