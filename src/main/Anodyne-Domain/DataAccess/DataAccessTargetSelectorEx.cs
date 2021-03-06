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

namespace Kostassoid.Anodyne.Domain.DataAccess
{
    using System;
    using Abstractions.DataAccess;
    using Abstractions.Dependency.Registration;
    using Common.CodeContracts;
    using Operations;

    public static class DataAccessTargetSelectorEx
    {
        public static void AsDomainStorage(this DataAccessTargetSelector targetSelector, Action<DomainDataAccessConfigurator> cc = null)
        {
            var container = targetSelector.ProviderSelector.Container;
            Requires.True(!container.Has<IUnitOfWorkManager>(), "Domain data access is already configured.");

            container.Put(Binding.Use(new UnitOfWorkManager(
                    new UnitOfWorkFactory(targetSelector.DataProvider.SessionFactory),
                    new ContainerOperationResolver(targetSelector.ProviderSelector.Container)
                )).As<IUnitOfWorkManager>());

            UnitOfWork.Initialize(container.Get<IUnitOfWorkManager>());

            if (cc != null)
                cc(new DomainDataAccessConfigurator());
        }
    }
}