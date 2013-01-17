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

using Kostassoid.Anodyne.Common.Reflection;
using Kostassoid.Anodyne.Node.Dependency;
using Kostassoid.Anodyne.Node.Dependency.Registration;
using Kostassoid.BlogNote.Web.Query;

namespace Kostassoid.BlogNote.Web.Code
{
    using Anodyne.Node;
    using Anodyne.Node.Configuration;

    public class QueryRegistration : IStartupAction
    {
        public void OnStartup(INodeConfiguration configuration)
        {
            configuration
                .Container
                .Put(Binding.Use(AllTypes.BasedOn<IQuery>(From.ThisAssembly)).With(Lifestyle.Unmanaged));
        }
    }
}