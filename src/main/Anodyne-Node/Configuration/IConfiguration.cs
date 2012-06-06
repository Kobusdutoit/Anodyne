﻿// Copyright 2011-2012 Anodyne.
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

namespace Kostassoid.Anodyne.Node.Configuration
{
    using Anodyne.DataAccess.Policy;
    using Common;
    using System;

    public interface IConfiguration : ISyntax
    {
/* not sure
        void ActAsApplicationServer();
        void ActAsClient();
*/

        void RunIn(RuntimeMode runtimeMode);

        void UseDataAccessPolicy(Action<DataAccessPolicy> policyAction);

        void ConfigureUsing<TConfiguration>() where TConfiguration : IConfigurationAction;
        void ConfigureUsing(Action<INodeInstance> configurationAction);

        void OnStartupPerform<TStartup>() where TStartup : IStartupAction;
        void OnStartupPerform(Action<INodeInstance> startupAction);

        void OnShutdownPerform<TShutdown>() where TShutdown : IShutdownAction;
        void OnShutdownPerform(Action<INodeInstance> shutdownAction);
    }
}