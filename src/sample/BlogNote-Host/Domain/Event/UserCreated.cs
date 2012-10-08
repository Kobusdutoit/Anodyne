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

namespace Kostassoid.BlogNote.Host.Domain.Event
{
    using Anodyne.Domain.Events;

    public class UserCreated : AggregateEvent<User, UserCreated.Payload>
    {
        public UserCreated(User aggregate, string name, string email)
            : base(aggregate, new Payload(name, email))
        {
        }

        public class Payload : EventPayload
        {
            public string Name { get; protected set; }
            public string Email { get; protected set; }

            public Payload(string name, string email)
            {
                Name = name;
                Email = email;
            }
        }

    }

}