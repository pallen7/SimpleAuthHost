using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;
using BrockAllen.MembershipReboot.Relational;
using IdentityServer3.Core;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.InMemory;
using IdentityServer3.MembershipReboot;
using System.Collections.Generic;
using System.Security.Claims;

namespace SimpleAuthHost.IdServer
{
    public static class Users
    {
        public static IUserService GetMRUsers()
        {
            var mrdb = new DefaultMembershipRebootDatabase("MembershipReboot");
            var usrAccRepos = new DefaultUserAccountRepository(mrdb);
            var mrc = new MembershipRebootConfiguration();
            mrc.RequireAccountVerification = false;
            var usrAcc = new UserAccountService(mrc,usrAccRepos);
            var usr = new MembershipRebootUserService<UserAccount>(usrAcc);
            return usr;
        }

        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "bob",
                    Password = "secret",
                    Subject = "1",               // This is the unique identifier for the user that will be embedded into the access token

                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Bob"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Smith"),
                        new Claim(Constants.ClaimTypes.Role, "Manager"),
                    }
                },
                new InMemoryUser
                {
                    Username = "alice",
                    Password = "secret",
                    Subject = "2",

                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Alice"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Cooper"),
                        new Claim(Constants.ClaimTypes.Role, "Operator"),
                    }
                }
            };
        }
    }
}