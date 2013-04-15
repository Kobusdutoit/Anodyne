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

namespace Kostassoid.Anodyne.Common.ExecutionContext
{
    using System.Runtime.Remoting.Messaging;

    public class DefaultContextProvider : IContextProvider
    {
        public void Set(string name, object value)
        {
            CallContext.SetData(name, value);
        }

        public object Find(string name)
        {
            return CallContext.GetData(name);
        }

        public void Release(string name)
        {
            CallContext.FreeNamedDataSlot(name);
        }
    }
}