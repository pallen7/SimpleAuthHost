using IdentityServer3.Core.Models;
using System.Collections.Generic;
using System.Web.Configuration;

namespace SimpleAuthHost.IdServer
{
    public static class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "The Client",
                    ClientId = "theclient",   
                    Enabled = true,

                    Flow = Flows.Implicit,

                    RedirectUris = new List<string>                 // This is where the respons is sent.. (Not a user redirection)
                    {
                        WebConfigurationManager.AppSettings["WorkSuiteURL"]
                    },

                    AllowAccessToAllScopes = true
                }
            };
        }
    }
}