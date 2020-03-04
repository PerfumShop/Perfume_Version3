using Microsoft.AspNet.Identity;
using S3Train.Domain;
using S3Train.Model.Role;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace S3Train.Contract
{
    public interface IRoleService
    {
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> CreateAsync(ApplicationRole role);
        Task<IdentityResult> UpdateAsync(ApplicationRole role);
        Task<IdentityResult> DeleteAsync(ApplicationRole role);
        Task<IPagedList<RoleViewModel>> GetRolesAsync(int pageIndex, int pageSize);
        Task<ApplicationRole> FindByIdAsync(string id);
        List<ApplicationRole> GetAllRoles();
    }
}
