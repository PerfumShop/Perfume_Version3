using S3.Train.WebPerFume.Models;
using S3.Train.WebPerFume.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S3.Train.WebPerFume.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        // GET: Checkout
        public ActionResult Index()
        {
            if (HttpContext.Request.Cookies["UserId"] != null)
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("UserId");

                CheckoutViewModel model = _checkoutService.GetCheckoutModel(cookie.Value);
                return View(model);
            }
            else
            {
                //error
                return View("error");
            }
        }
        [HttpPost]
        public ActionResult Order(CheckoutViewModel model)
        {
            if (HttpContext.Request.Cookies["UserId"] != null)
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("UserId");
                bool result = _checkoutService.SaveOrder(model, cookie.Value);
                if (result)
                {
                    //success
                    return RedirectToAction("Success",
                    new { controller = "Checkout", action = "Success" });
                }
            }
            //error
            return View("error");
        }
        public ActionResult Success()
        {
            return View("OrderSuccess");
        }

    }
}