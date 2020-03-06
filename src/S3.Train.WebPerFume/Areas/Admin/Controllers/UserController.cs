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
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController()
        {

        }
        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        // GET: Admin/User
        public async Task<ActionResult> Index()
        {
            var model = await _userService.GetUserAsync(1, 10);
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.Roles = DropDownRole();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel model)
        {
            var user = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Avatar = model.Avatar,
                UserName = model.UserName
            };
            await _userService.CreateAsync(user, model.PassWord);
            await _userService.UserAddToRolesAsync(user.Id, model.Role);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Update(string id)
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

        [HttpPost]
        public async Task<ActionResult> Update(UserViewModel model)
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

        public async Task<ActionResult> Delete(string id)
        {
            await _userService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private List<SelectListItem> DropDownRole()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var roles = _roleService.GetAllRoles();
            foreach (var role in roles)
                list.Add(new SelectListItem() { Value = role.Name, Text = role.Name });
            return list;
        }
    }
}