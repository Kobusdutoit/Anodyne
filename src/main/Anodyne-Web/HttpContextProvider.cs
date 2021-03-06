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

namespace Kostassoid.Anodyne.Web
{
    using System;
    using System.Web;

    using Common.ExecutionContext;

    public class HttpContextProvider : IContextProvider
    {
        public void Set(string name, object value)
        {
            EnsureContextIsAvailable();
            HttpContext.Current.Items[name] = value;
        }

        public object Find(string name)
        {
            EnsureContextIsAvailable();
            return HttpContext.Current.Items.Contains(name) ? HttpContext.Current.Items[name] : null;
        }

        public void Release(string name)
        {
            EnsureContextIsAvailable();
            HttpContext.Current.Items.Remove(name);
        }

        private static void EnsureContextIsAvailable()
        {
            if (HttpContext.Current != null) return;

            throw new InvalidOperationException("HttpContext is not available. Make sure you don't work with context outside of http application request.");
        }
    }
}