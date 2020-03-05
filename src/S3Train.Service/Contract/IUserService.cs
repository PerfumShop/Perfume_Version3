using Microsoft.AspNet.Identity;
using S3Train.Domain;
using S3Train.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace S3Train.Contract
{
    public interface IUserService
    {
        Task<IList<string>> GetRolesForUserAsync(string userId);
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<ApplicationUser> GetUserByUserNameAsync(string userName);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<ApplicationUser> GetUserByUserNameAndPasswordAsync(string userName, string password);
        Task<IdentityResult> UpdatePasswordAsync(string id, string password);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<IdentityResult> UpdateAsync(ApplicationUser user);
        Task<IList<UserLoginInfo>> GetLoginsAsync(string id);
        Task SignInAsync(ApplicationUser user, bool isPersistent, bool shouldLockout = false);
        void SignOut();
        Task<string> GeneratePasswordResetTokenAsync(string userId);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
        Task<IdentityResult> ResetPasswordAsync(string userId, string token, string password);

        Task<string> GenerateEmailConfirmationTokenAsync(string userId);
        Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string applicationType);
        Task<IdentityResult> UserAddToRolesAsync(string userId, params string[] roles);
        Task<IdentityResult> RemoveFromRolesAsync(string userId, params string[] roles);
        Task<IPagedList<UserViewModel>> GetUserAsync(int pageIndex, int pageSize);
        
    }
}
