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

namespace Kostassoid.Anodyne.Abstractions.Dependency
{
    using System;

    /// <summary>
    /// Single component type binding specification.
    /// </summary>
    public class SingleBinding : LifestyleBasedBinding
    {
        /// <summary>
        /// Component service type.
        /// </summary>
        public Type Service { get; protected set; }
        /// <summary>
        /// Implementation resolver.
        /// </summary>
        public IImplementationResolver Resolver { get; protected set; }
        /// <summary>
        /// Optional name for component.
        /// </summary>
        public string Named { get; protected set; }

        internal SingleBinding(Type service)
        {
            Service = service;
        }

        internal void SetResolver(IImplementationResolver resolver)
        {
            Resolver = resolver;
        }

        internal void SetName(string name)
        {
            Named = name;
        }
    }
}