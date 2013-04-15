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

namespace Kostassoid.Anodyne.EventStore.Adapters
{
    using System;
    using Domain.Events;

    [Serializable]
    public class EventEnvelope
    {
        public Guid Id { get; private set; }
        public string TargetType { get; private set; }
        public object TargetId { get; private set; }
        public long TargetVersion { get; private set; }
        public string EventType { get; private set; }
        public IAggregateEvent Raw { get; private set; }

        public EventEnvelope(IAggregateEvent @event)
        {
            Raw = @event;

            Id = @event.Id;
            TargetType = @event.Target.GetType().Name;
            TargetId = @event.Target.IdObject;
            TargetVersion = @event.TargetVersion;
            EventType = @event.GetType().Name;
        }
    }
}