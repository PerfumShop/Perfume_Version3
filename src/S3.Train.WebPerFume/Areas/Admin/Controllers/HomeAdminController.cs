using Newtonsoft.Json;
using S3Train.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S3.Train.WebPerFume.Areas.Admin.Controllers
{
    [Authorize(Users = "Admin")]
    public class HomeAdminController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        #region Ctor
        public HomeAdminController()
        {

        }

        public HomeAdminController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        #endregion

        #region Index DeleteCart And Error500
        // GET: Admin/Home
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch { return RedirectToAction("Erorr500"); }
        }

        // Delete carts null and have create date than 30 days
        public ActionResult DeleteCartNullOrThan30Days()
        {
            try
            {
                var carts = _shoppingCartService.GetShoppingCartNullOrThan30Days();
                foreach (var item in carts)
                {
                    _shoppingCartService.Delete(item);
                }
                return RedirectToAction("index");
            }
            catch { return RedirectToAction("Erorr500"); }
        }

        public ActionResult Erorr500()
        {
            return View();
        }
        #endregion
    }
}