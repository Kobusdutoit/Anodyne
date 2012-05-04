// Copyright 2011-2012 Anodyne.
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

namespace Kostassoid.Anodyne.Wiring.Syntax.Concrete
{
    using System;
    using Common;
    using Subscription;

    internal class TargetDiscoverySyntax<TEvent, THandler> : ITargetDiscoverySyntax<TEvent, THandler>, ISyntax
        where TEvent : class, IEvent
        where THandler : class
    {
        private readonly SubscriptionSpecification<TEvent> _specification;

        public TargetDiscoverySyntax(SubscriptionSpecification<TEvent> specification)
        {
            _specification = specification;

            _specification.TargetType = typeof (THandler);
        }

        public Action As(Func<TEvent, Option<THandler>> discoveryFunc)
        {
            _specification.TargetDiscoveryFunction =
                e =>
                {
                    var discoveredTarget = discoveryFunc(e);
                    if (discoveredTarget.IsNone)
                        throw new InvalidOperationException(string.Format("Unable to discover the target of type {0}", typeof (THandler).Name));

                    return discoveredTarget.Value;
                };

            return SubscriptionPerformer.Perform(_specification);
        }
    }
}