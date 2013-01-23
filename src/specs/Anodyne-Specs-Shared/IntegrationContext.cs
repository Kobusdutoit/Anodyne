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
// 

namespace Kostassoid.Anodyne.Specs.Shared
{
    using Domain.DataAccess;
    using Node;
    using Node.Configuration;
    using Windsor;

    public static class IntegrationContext
    {
        public static Node System;

        class TestSystem : Node
        {
            public override void OnConfigure(INodeConfigurator c)
            {
                c.UseWindsorContainer();
                c.UseInMemoryDataAccess().AsDomainStorage();
            }
        }

        public static void Init()
        {
            System = new TestSystem();
            System.Start();
        }

    }
}