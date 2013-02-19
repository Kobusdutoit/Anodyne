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

namespace Kostassoid.Anodyne.RavenDb
{
    using Abstractions.DataAccess;
    using Common.CodeContracts;
    using System;
    using Raven.Client;

    public static class DataAccessContextEx
    {
        private static IDocumentSession GetMongoNativeSessionFrom(IDataAccessContext dataAccessContext)
        {
            var nativeSession = dataAccessContext.GetSession().NativeSession;
            Requires.True(nativeSession is IDocumentSession, message: string.Format("Expected IDocumentSession, not {0}.", nativeSession.GetType().Name));

            return (IDocumentSession) nativeSession;
        }

        public static void OnNative(this IDataAccessContext dataAccessContext, Action<IDocumentSession> nativeAction)
        {
            nativeAction(GetMongoNativeSessionFrom(dataAccessContext));
        }

        public static TResult OnNative<TResult>(this IDataAccessContext dataAccessContext, Func<IDocumentSession, TResult> nativeFunc)
        {
            return nativeFunc(GetMongoNativeSessionFrom(dataAccessContext));
        }
    }
}