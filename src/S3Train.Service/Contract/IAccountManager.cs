using Microsoft.Owin.Security;
using S3Train.Domain;
using S3Train.IdentityManager;
using System.Linq;

namespace S3Train.Contract
{
    public interface IAccountManager
    {
        IAuthenticationManager AuthenticationManager { get; }
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RolesManager { get; }
        ApplicationSignInManager SignInManager { get; }
        IQueryable<ApplicationUser> GetQueryableUser { get; }
        IQueryable<ApplicationRole> GetQueryableRole { get; }

        IQueryable<T> Query<T>() where T : class;
    }
}
