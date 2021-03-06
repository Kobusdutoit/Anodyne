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

namespace Kostassoid.BlogNote.Host.Domain
{
    using Anodyne.Common;
    using Anodyne.Common.CodeContracts;
    using Anodyne.Common.Tools;
    using Anodyne.Domain.Base;
    using System;
    using Event;

    public class User : AggregateRoot<Guid>
    {
        public string Name { get; protected set; }
        public string Email { get; protected set; }

        public DateTime Registered { get; protected set; }

        public uint Posts { get; protected set; }

        #region Creation

        protected User()
        {
            Id = SeqGuid.NewGuid();
            Posts = 0;
        }

        public static User Create(string name, string email)
        {
            Requires.NotNullOrEmpty(name, "name");

            var user = new User();

            Apply(new UserCreated(user, name, email));

            return user;
        }

        protected void OnCreated(UserCreated @event)
        {
            Name = @event.Name;
            Email = @event.Email;

            Registered = SystemTime.Now;
        }

        #endregion

        #region Posts Update

        public void UpdatePosts(uint posts)
        {
            Requires.True(posts >= Posts, "posts", "We can't actually delete posts, by design");

            Apply(new UserPostsUpdated(this, posts));
        }

        protected void OnPostsUpdated(UserPostsUpdated @event)
        {
            Posts = @event.Posts;
        }

        #endregion
         
    }
}