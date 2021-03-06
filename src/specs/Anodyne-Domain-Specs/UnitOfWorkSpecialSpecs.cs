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

namespace Kostassoid.Anodyne.Domain.Specs
{
    using Anodyne.Specs.Shared;
    using Base;
    using DataAccess;
    using DataAccess.Exceptions;
    using DataAccess.Policy;
    using Events;
    using System;
    using Common.Tools;
    using FluentAssertions;
    using NUnit.Framework;

    // ReSharper disable InconsistentNaming
    public class UnitOfWorkSpecialSpecs
    {
        public class UnitOfWorkScenario
        {
            public UnitOfWorkScenario()
            {
                IntegrationContext.Init();
            }
        }

        [Serializable]
        public class TestRoot : AggregateRoot<Guid>
        {
            protected TestRoot()
            {
                Id = SeqGuid.NewGuid();
            }

            public static TestRoot Create()
            {
                var root = new TestRoot();
                Apply(new TestRootCreated(root));
                return root;
            }

            protected void OnCreated(TestRootCreated @event)
            {
                Update(); // prohibited
            }

            public void Update()
            {
                Apply(new TestRootUpdated(this));
            }

            protected void OnUpdated(TestRootUpdated @event)
            {

            }


        }

        public class TestRootCreated : AggregateEvent<TestRoot>
        {
            public TestRootCreated(TestRoot target)
                : base(target)
            {
            }
        }

        public class TestRootUpdated : AggregateEvent<TestRoot>
        {
            public TestRootUpdated(TestRoot target)
                : base(target)
            {
            }
        }

        [TestFixture]
        [Category("Unit")]
        public class when_calling_nested_apply : UnitOfWorkScenario
        {
            [Test]
            public void should_throw_concurrency_exception()
            {
                using (UnitOfWork.Start(StaleDataPolicy.Ignore))
                {
                    Action action = () => TestRoot.Create();
                    action.ShouldThrow<ConcurrencyException>();
                }
            }
        }
    }
    // ReSharper restore InconsistentNaming

}
