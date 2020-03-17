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

        public CartController()
        {

        }

        public CartController(IProductService productService, IProductVariationService productVariationService,
            IShoppingCartService shoppingCartService, IShoppingCartDetailService shoppingCartDetailService)
        {
            _productService = productService;
            _productVariationService = productVariationService;
            _shoppingCartService = shoppingCartService;
            _shoppingCartDetailService = shoppingCartDetailService;
        }

        // GET: Cart
        public ActionResult Index()
        {
            var cart = GetOrSetShoppingCart();
            var model = GetShoppingCartModel(_shoppingCartService.GetShoppingCartByUserId(cart.UserId));
            var model2 = GetShoppingCartDetailModels(model.ShoppingCartDetails);
            return View(model2);
        }

        [HttpPost]
        public ActionResult AddProductToCart(string proVaId, int quantity)
        {
            var shoppingCart = GetOrSetShoppingCart();
            var id = Guid.Parse(proVaId);
            var shoppingCartDetail = _shoppingCartDetailService.GetByProductIdAndCartShoppingCartId(id, shoppingCart.Id);
            var productVa = _productVariationService.GetById(id);
            var productPrice = GetProductPrice(productVa); // get price of product

            if (shoppingCartDetail != null)
            {
                // update quantity and update day
                shoppingCartDetail.Quantity = quantity;
                shoppingCartDetail.UpdatedDate = DateTime.Now;
                shoppingCartDetail.TotalPrice = productPrice * quantity;
                _shoppingCartDetailService.Update(shoppingCartDetail);
            }
            else
            {
                // Add new shopping cart detail
                var cartDetail = new ShoppingCartDetail
                {
                    Id = Guid.NewGuid(),
                    ShoppingCart_Id = shoppingCart.Id,
                    ProductVariation_Id = id,
                    Quantity = quantity,
                    CreatedDate = DateTime.Now,
                    TotalPrice = productPrice * quantity,
                    IsActive = true,
                };
                _shoppingCartDetailService.Insert(cartDetail);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateCart(List<ShoppingCartDetailModel> model)
        {
            if (model != null)
            {
                foreach (var item in model)
                {
                    var pro = _shoppingCartDetailService.GetById(item.Id);
                    var proVa = _productVariationService.GetById(pro.ProductVariation_Id);
                    pro.Quantity = item.Quantity;
                    pro.UpdatedDate = DateTime.Now;
                    pro.TotalPrice = item.Quantity * GetProductPrice(proVa);
                    _shoppingCartDetailService.Update(pro);
                }
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Delete product in cart by shopping cart detail id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteProductOnCart(Guid id)
        {
            var model = _shoppingCartDetailService.GetById(id);
            if (model != null)
                _shoppingCartDetailService.Delete(model);

            return RedirectToAction("Index");
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
            if (cookie != null)
            {
                return cookie.Value;
            }
            else
            {
                HttpCookie userId = new HttpCookie(name)
                {
                    Value = CheckedUserAuth()
                };
                userId.Expires.AddDays(3); // Cookie will be Expires after 3 day
                Response.Cookies.Add(userId);

                return userId.Value;
            }
        }

        /// <summary>
        /// Get Or Set ShoppingCart
        /// </summary>
        /// <returns>Shopping Cart</returns>
        private ShoppingCart GetOrSetShoppingCart()
        {
            var userId = CheckedCookie("UserId");
            var shoppingCart = _shoppingCartService.GetShoppingCartByUserId(userId);
            if (shoppingCart != null)
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

        /// <summary>
        /// Product Price
        /// </summary>
        /// <param name="productVariation"></param>
        /// <returns>price</returns>
        private decimal GetProductPrice(ProductVariation productVariation)
        {
            if (productVariation.DiscountPrice > 0 && productVariation.DiscountPrice != null)
                return Convert.ToDecimal(productVariation.DiscountPrice);
            else
                return productVariation.Price;
        }

        private IList<ShoppingCartDetailModel> GetShoppingCartDetailModels(ICollection<ShoppingCartDetail> shoppingCartDetails)
        {
            return shoppingCartDetails.Select(x => new ShoppingCartDetailModel
            {
                Id = x.Id,
                CreatedDate = x.CreatedDate,
                ProductVariation = x.ProductVariation,
                ProductVariation_Id = x.ProductVariation_Id,
                Quantity = x.Quantity,
                ShoppingCart = x.ShoppingCart,
                ShoppingCart_Id = x.ShoppingCart_Id,
                UpdatedDate = x.UpdatedDate,
                TotalPrice = x.TotalPrice
            }).OrderBy(p => p.CreatedDate).ToList();
        }

        private ShoppingCartModel GetShoppingCartModel(ShoppingCart shoppingCart)
        {
            var model = new ShoppingCartModel
            {
                Id = shoppingCart.Id,
                CreatedDate = shoppingCart.CreatedDate,
                UserId = shoppingCart.UserId,
                OrderDate = shoppingCart.OrderDate,
                Orders = shoppingCart.Orders,
                ShoppingCartDetails = _shoppingCartDetailService.GetAllByCartId(shoppingCart.Id),
                TotalPrice = shoppingCart.TotalPrice,
                UpdatedDate = shoppingCart.UpdatedDate
                
            };
            return model;
        }
    }
}