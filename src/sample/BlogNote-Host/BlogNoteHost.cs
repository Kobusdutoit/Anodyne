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

namespace Kostassoid.BlogNote.Host
{
    using Anodyne.Domain.DataAccess;
    using Anodyne.Node;
    using Anodyne.Node.Configuration;
    using Anodyne.Windsor;
    using Anodyne.MongoDb;
    using Startup;

    public class BlogNoteHost : Node
    {
        public override void OnConfigure(INodeConfigurator c)
        {
            c.UseWindsorContainer();
            c.UseWindsorWcfProxyFactory();

			c.ForDataAccess().UseMongoDatabase(Anodyne.Node.Configuration.Configured.From.AppSettings("ConnectionString")).AsDomainStorage();

            c.OnStartupPerform<DataAccessConfiguration>();
            c.OnStartupPerform<WcfServicesRegistration>();
        }
    }
}