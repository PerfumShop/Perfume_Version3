using S3.Train.WebPerFume.Models;
using S3Train.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Collections.Specialized;
using S3Train.Domain;

namespace S3.Train.WebPerFume.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductVariationService _productVariationService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IShoppingCartDetailService _shoppingCartDetailService;

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult AddProductToCart(Guid proVaId, int quantity)
        {
            var shoppingCart = CreateShoppingCart();
            var shoppingCartDetail = _shoppingCartDetailService.GetByProductIdAndCartShoppingCartId(proVaId, shoppingCart.Id);

            if(shoppingCartDetail != null)
            {
                // update quantity and update day
                shoppingCartDetail.Quantity = quantity;
                shoppingCartDetail.UpdatedDate = DateTime.Now;

                _shoppingCartDetailService.Update(shoppingCartDetail);
            }
            else
            {
                // Add new shopping cart detail
                var cartDetail = new ShoppingCartDetail
                {
                    Id = Guid.NewGuid(),
                    ShoppingCart_Id = shoppingCart.Id,
                    ProductVariation_Id = proVaId,
                    Quantity = quantity,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                };
                _shoppingCartDetailService.Insert(cartDetail);
            }
            return View();
        }

        /// <summary>
        /// Checked User Auth
        /// </summary>
        /// <returns>User Id</returns>
        private string CheckedUserAuth()
        {
            if (Request.IsAuthenticated)
                return User.Identity.GetUserId();
            else
                return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Check cookie
        /// </summary>
        /// <param name="name">cookie name</param>
        /// <returns>User Id</returns>
        private string CheckedCookie(string name)
        {
            var cookie = Request.Cookies[name];
            if(cookie != null)
            {
                return cookie.Value;
            }
            else
            {
                HttpCookie userId = new HttpCookie(name)
                {
                    Value = CheckedUserAuth()
                };
                userId.Expires.AddMinutes(3); // Cookie will be Expires after 3 minute
                Response.Cookies.Add(userId);

                return userId.Value;
            }
        }

        /// <summary>
        /// Create Shopping Cart
        /// </summary>
        /// <returns>Shopping Cart Id</returns>
        private ShoppingCart CreateShoppingCart()
        {
            var userId = CheckedCookie("UserId");
            var shoppingCart = _shoppingCartService.GetShoppingCartByUserId(userId);
            if(shoppingCart != null)
            {
                return shoppingCart;
            }
            else
            {
                var model = new ShoppingCart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    TotalPrice = 0,
                    IsActive = true
                };
                _shoppingCartService.Insert(model);

                return model;
            }
        }
    }
}