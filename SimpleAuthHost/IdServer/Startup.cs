using IdentityManager.Configuration;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using Microsoft.Owin;
using Owin;
using System;
using System.Security.Cryptography.X509Certificates;
using SimpleAuthHost.IdManager;

[assembly: OwinStartup(typeof(SimpleAuthHost.IdServer.Startup))]

namespace SimpleAuthHost.IdServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Id Manager
            var factory = new IdentityManagerServiceFactory();

            factory.Configure();

            var idmoptions = new IdentityManagerOptions
            {
                Factory = factory
            };

            app.UseIdentityManager(idmoptions);

            // Id Server
            var fct = new IdentityServerServiceFactory()
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryScopes(Scopes.Get());

            fct.UserService = new IdentityServer3.Core.Configuration.Registration<IUserService>(r => Users.GetMRUsers());

            var options = new IdentityServerOptions
            {
                SiteName = "Authorization Server",
                SigningCertificate = LoadCertificate(),
                Factory = fct,
            };

            app.UseIdentityServer(options);
        }

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\IdServer\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}