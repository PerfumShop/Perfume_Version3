using Microsoft.AspNet.Identity.Owin;
using S3.Train.WebPerFume.Areas.Admin.Models;
using S3Train.Domain;
using S3Train.IdentityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace S3.Train.WebPerFume.Areas.Admin.Controllers
{
    [Authorize(Users = "Admin")]
    public class RoleController : Controller
    {
        private ApplicationRoleManager _roleManager;

        #region Ctor
        public RoleController()
        {
        }

        public RoleController(ApplicationRoleManager roleManager)
        {
            roleManager = _roleManager;
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        #endregion

        #region Index 
        // GET: Admin/Role
        public ActionResult Index()
        {
            try
            {
                List<RoleViewModel> list = new List<RoleViewModel>();

                foreach (var role in RoleManager.Roles)
                    list.Add(new RoleViewModel(role));
                return View(list);
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion

        #region Create, edit, detail and delete role
        public PartialViewResult Create()
        {
            return PartialView("~/Areas/Admin/Views/Role/_CreateAndEditRolePartial.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel model)
        {
            try
            {
                var role = new ApplicationRole() { Id = Guid.NewGuid().ToString(), Name = model.Name, Description = model.Description };
                await RoleManager.CreateAsync(role);
                return RedirectToAction("Index");
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                var role = await RoleManager.FindByIdAsync(id);
                return PartialView("~/Areas/Admin/Views/Role/_CreateAndEditRolePartial.cshtml", new RoleViewModel(role));
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModel model)
        {
            try
            { 
                var role = new ApplicationRole() { Id = model.Id, Name = model.Name };
                await RoleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        public async Task<ActionResult> Details(string id)
        {
            try
            {
                var role = await RoleManager.FindByIdAsync(id);
                return View(new RoleViewModel(role));
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var role = await RoleManager.FindByIdAsync(id);
                return View(new RoleViewModel(role));
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        public async Task<ActionResult> DeleteCofirmed(string id)
        {
            try
            {
                var role = await RoleManager.FindByIdAsync(id);
                await RoleManager.DeleteAsync(role);
                return RedirectToAction("Index");
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion
    }
}