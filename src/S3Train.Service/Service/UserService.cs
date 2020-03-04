using Microsoft.AspNet.Identity;
using S3Train.Contract;
using S3Train.Domain;
using S3Train.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace S3Train.Service
{
    public class UserService : IUserService
    {
        

        public Task<IdentityResult> ConfirmEmail(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> Create(ApplicationUser user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string applicationType)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateEmailConfirmationTokenAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GeneratePasswordResetToken(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLogins(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedList<UserViewModel>> GetUser(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetUserById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetUserByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetUserByUserNameAndPassword(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> RemoveFromRoles(string userId, params string[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> ResetPassword(string userId, string token, string password)
        {
            throw new NotImplementedException();
        }

        public Task SignInAsync(ApplicationUser user, bool isPersistent, bool shouldLockout = false)
        {
            throw new NotImplementedException();
        }

        public void SignOut()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> Update(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdatePassword(string id, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UserAddToRoles(string userId, params string[] roles)
        {
            throw new NotImplementedException();
        }
    }
}
