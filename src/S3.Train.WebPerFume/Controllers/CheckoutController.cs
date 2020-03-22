using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using S3.Train.WebPerFume.CommonFunction;
using S3.Train.WebPerFume.Models;
using S3.Train.WebPerFume.Services;
using S3Train.Contract;
using S3Train.Domain;
using S3Train.IdentityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace S3.Train.WebPerFume.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IShoppingCartDetailService _shoppingCartDetailService;
        private readonly IOrderService _orderService;
        private readonly IOrderDetailServicecs _orderDetailServicecs;
        private readonly IUserService _userService;
        private readonly IProductVariationService _productVariationService;
        private ApplicationUserManager _userManager;

        public CheckoutController()
        {
            
        }

        public CheckoutController(IShoppingCartService shoppingCartService, IShoppingCartDetailService shoppingCartDetailService, 
            IOrderService orderService, IOrderDetailServicecs orderDetailServicecs, IUserService userService, IProductVariationService productVariationService)
        {
            _shoppingCartService = shoppingCartService;
            _shoppingCartDetailService = shoppingCartDetailService;
            _orderService = orderService;
            _orderDetailServicecs = orderDetailServicecs;
            _userService = userService;
            _productVariationService = productVariationService;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Checkout
        public async Task<ActionResult> Index()
        {
            try
            {
                var cookie = Request.Cookies["UserId"].Value;
                if (cookie != null)
                {
                    var cart = _shoppingCartService.GetShoppingCartByUserId(cookie);
                    var user = await _userService.GetUserByIdAsync(cookie);
                    var products = ConvertDomainToModel.shoppingCartDetailModels(_shoppingCartDetailService.GetAllByCartId(cart.Id));

                    var checkOutModel = new CheckoutViewModel()
                    {
                        shoppingCartDetailModels = products,
                        customerModel = GetCustomerModel(user)
                    };
                    return View(checkOutModel);
                }
                else
                {
                    return RedirectToAction("Index", "Cart");
                }
            }
            catch
            {
                return RedirectToAction("Erorr","Home");
            }
        }

        private CustomerModel GetCustomerModel(ApplicationUser user)
        {
            var model = new CustomerModel();
            if (user != null)
            {
                model.Email = user.Email;
                model.Address = user.Address;
                model.Name = user.FullName;
                model.Phone = user.PhoneNumber;
                return model;
            }
            else
            {
                return model;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Order(CheckoutViewModel model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(model);
                }

                var cookie = Request.Cookies["UserId"].Value;
                var cart = _shoppingCartService.GetShoppingCartByUserId(cookie); // Search Shoppping Cart by user id on cookie
                decimal subTotal = SumProductPriceInOrder(model.shoppingCartDetailModels); // Total price of order

                // create new order
                var order = new Order()
                {
                    Id = Guid.NewGuid(),
                    DeliveryName = model.customerModel.Name,
                    DeliveryAddress = model.customerModel.Address,
                    DeliveryPhone = model.customerModel.Phone,
                    Email = model.customerModel.Email,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Status = SetOrderStatus(OrderStatus.Receive),
                    OrderDate = DateTime.Now,
                    Note = model.customerModel.Note,
                    SubPrice = subTotal,
                    ToatalPrice = subTotal + 1,
                    DeliveryFee = 1
                };
                _orderService.Insert(order);

                //Add Order Detail for order
                AddOrderDetails(model.shoppingCartDetailModels, order.Id);
                // Update Quanlity of product variation
                UpdateQuanlityProductVariation(model.shoppingCartDetailModels);
                // Delete ShoppingCart
                _shoppingCartService.Delete(cart);

                // Send email
                await UserManager.SendEmailAsync(User.Identity.GetUserId(), "Confirm Order From PerfumeShop","Thank You");
                return RedirectToAction("Success","Checkout");
            }
            catch
            {
                //error
                return RedirectToAction("Error","Home");
            }
        }

        /// <summary>
        /// Sum Product Price In Order 
        /// </summary>
        /// <param name="models"></param>
        /// <returns>sum price</returns>
        private decimal SumProductPriceInOrder(IList<ShoppingCartDetailModel> models)
        {
            decimal sum = 0;
            foreach (var item in models)
            {
                var pro = _productVariationService.GetById(item.ProductVariation_Id);
                sum += GetPrice.GetProductPrice(pro);
            }
            return sum;
        }

        /// <summary>
        /// Add OrderDetail 
        /// </summary>
        /// <param name="models"></param>
        /// <param name="orderId"></param>
        private void AddOrderDetails(IList<ShoppingCartDetailModel> models, Guid orderId)
        {
            foreach (var item in models)
            {
                var cartDetail = _shoppingCartDetailService.GetById(item.Id);
                var oderDetail = new OrderDetail()
                {
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    ProductVariation_ID = cartDetail.ProductVariation_Id,
                    Quantity = cartDetail.Quantity,
                    Oder_Id = orderId,
                    TotalPrice = GetPrice.GetProductPrice(cartDetail.ProductVariation)
                };
                _orderDetailServicecs.Insert(oderDetail);
            }
        }

        public ActionResult Success()
        {
            return View("OrderSuccess");
        }

        private void UpdateQuanlityProductVariation(IList<ShoppingCartDetailModel> models)
        {
            if(models != null)
            {
                foreach(var item in models)
                {
                    var proVa = _productVariationService.GetById(item.ProductVariation_Id);
                    var cartDetail = _shoppingCartDetailService.GetById(item.Id);
                    proVa.StockQuantity = proVa.StockQuantity - cartDetail.Quantity;

                    _productVariationService.Update(proVa);
                }
            }
        }

        // set Order status for oder
        private string SetOrderStatus(OrderStatus orderStatus)
        {
            string status = "";
            switch (orderStatus)
            {
                case OrderStatus.Receive:
                    status = "Receive Order";
                    break;
                case OrderStatus.Confirm:
                    status = "Confirm Order";
                    break;
                case OrderStatus.TakeProduct:
                    status = "Take Product";
                    break;
                case OrderStatus.Delivery:
                    status = "Delivery Order";
                    break;
                case OrderStatus.Success:
                    status = "Success Order";
                    break;
                case OrderStatus.Cancel:
                    status = "Cancel";
                    break;
                default:
                    status = "No Status Order";
                    break;
            }
            return status;
        }
    }
}