using S3Train.Contract;
using S3Train.Domain;
using S3Train.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace S3.Train.WebPerFume.Areas.Admin.Controllers
{
    [Authorize(Users = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        #region Ctor
        public UserController()
        {

        }
        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
        #endregion

        #region Index
        // GET: Admin/User
        public async Task<ActionResult> Index()
        {
            try
            {
                var model = await _userService.GetUserAsync(1, 10);
                return View(model);
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion

        #region Create, edit, detail and delete user
        public ActionResult Create()
        {
            try
            {
                ViewBag.Roles = DropDownRole();
                return View();
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel model)
        {
            try
            {
                var user = new ApplicationUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = model.Email,
                    UserName = model.UserName,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    Avatar = model.Avatar
                };
                await _userService.CreateAsync(user, model.PassWord);
                await _userService.UserAddToRolesAsync(user.Id, model.Role);
                return RedirectToAction("Index");
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        public async Task<ActionResult> Update(string id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                ViewBag.Roles = DropDownRole();
                var model = new UserViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Avatar = user.Avatar,
                    UserName = user.UserName
                };
                return View(model);
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        [HttpPost]
        public async Task<ActionResult> Update(UserViewModel model)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(model.Id);

                if (user == null)
                    return View(model);

                user.Email = model.Email;
                user.FullName = model.FullName;
                user.PhoneNumber = model.PhoneNumber;
                user.Avatar = model.Avatar;
                user.UserName = model.UserName;

                await _userService.UpdateAsync(user);
                return RedirectToAction("Index");
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        private List<SelectListItem> DropDownRole()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var roles = _roleService.GetAllRoles();
            foreach (var role in roles)
                list.Add(new SelectListItem() { Value = role.Name, Text = role.Name });
            return list;
        }
        #endregion
    }
}