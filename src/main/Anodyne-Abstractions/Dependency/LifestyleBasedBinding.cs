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
    /// <summary>
    /// Lifestyle-based binding.
    /// </summary>
    public abstract class LifestyleBasedBinding : IBinding
    {
        /// <summary>
        /// Defined lifestyle.
        /// </summary>
        public Lifestyle Lifestyle { get; protected set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected LifestyleBasedBinding()
        {
            Lifestyle = Lifestyle.ProviderDefault;
        }

        /// <summary>
        /// Set component lifestyle.
        /// </summary>
        /// <param name="lifestyle">Desired lifestyle for a component.</param>
        public void SetLifestyle(Lifestyle lifestyle)
        {
            Lifestyle = lifestyle;
        }
    }
}