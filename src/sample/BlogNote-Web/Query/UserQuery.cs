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

using Kostassoid.Anodyne.DataAccess;

namespace Kostassoid.BlogNote.Web.Query
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Persistent;

    public class UserQuery : IQuery
    {
        private readonly IDataAccessContext _dataAccessContext;

        public UserQuery(IDataAccessContext dataAccessContext)
        {
            _dataAccessContext = dataAccessContext;
        }

        public IList<User> GetAll()
        {
            return _dataAccessContext.Query<User>().ToList();
        }

        public User GetOne(Guid user)
        {
            return _dataAccessContext.Query<User>().FirstOrDefault(u => u.Id == user);
        }

    }
}