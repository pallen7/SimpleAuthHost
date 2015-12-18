using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;
using BrockAllen.MembershipReboot.Relational;
using IdentityManager.Configuration;
using IdentityManager.MembershipReboot;

namespace SimpleAuthHost.IdManager
{
    public static class IdManagerExtensions
    {
        public static void Configure(this IdentityManagerServiceFactory factory)
        {
            var mrdb = new DefaultMembershipRebootDatabase("MembershipReboot");
            var usrAccRepos = new DefaultUserAccountRepository(mrdb);
            var defaultGrpRepos = new DefaultGroupRepository(mrdb);
            var mrc = new MembershipRebootConfiguration<RelationalUserAccount>();
            mrc.RequireAccountVerification = false;
            var usrAcc = new UserAccountService<RelationalUserAccount>(mrc,usrAccRepos);
            var grpSvc = new GroupService<RelationalGroup>(defaultGrpRepos);
            var managerService = new MembershipRebootIdentityManagerService<RelationalUserAccount,RelationalGroup>(usrAcc, grpSvc);

            factory.IdentityManagerService = new Registration<IdentityManager.IIdentityManagerService>(managerService);
        }
    }
}