using S3.Train.WebPerFume.Areas.Admin.Models;
using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S3.Train.WebPerFume.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IShoppingCartService _shoppingcartService;

        public OrderController() { }

        public OrderController(IOrderService orderService, IShoppingCartService shoppingcartService)
        {
            _orderService = orderService;
            _shoppingcartService = shoppingcartService;
        }

        // GET: Admin/Order
        public ActionResult Index()
        {
            var model = GetOrders(_orderService.SelectAll());
            return View(model);
        }

        public ActionResult DetailProduct(Guid id)
        {
            var order = _orderService.GetOrderById(id);
            var model = new OrderViewModel
            {
                Id = order.Id,
                DeliveryAddress = order.DeliveryAddress,
                DeliveryName = order.DeliveryName,
                DeliveryPhone = order.DeliveryPhone,
                OrderDate = order.OrderDate,
                CreateDate = order.CreatedDate,
                IsActive = order.IsActive,
                DeliveryFee = order.DeliveryFee,
                Email = order.Email,
                Note = order.Note,
                Status = order.Status,
                SubPrice = order.SubPrice,
                ToatalPrice = order.ToatalPrice,
                OrderDetails = order.OrderDetails
            };

            return View(model);
        }

        [HttpGet]
        public PartialViewResult DeleteOrder(Guid id)
        {
            var order = _orderService.GetById(id);
            var model = new OrderViewModel
            {
                DeliveryName = $"{order.DeliveryName}"
            };
            return PartialView("~/Areas/Admin/Views/Order/_DeleteOrder.cshtml", model);
        }

        [HttpPost]
        public ActionResult DeleteOrder(ProductViewModel model)
        {
            var order = _orderService.GetById(model.Id);
            _orderService.Delete(order);
            return RedirectToAction("Index");
        }


        /// <summary>
        /// Convert List Product to List ProductViewModel All Properties
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public IList<OrderViewModel> GetOrders(IList<Order> orders)
        {
            return orders.Select(x => new OrderViewModel
            {
                Id = x.Id,
                DeliveryAddress = x.DeliveryAddress,
                DeliveryName = x.DeliveryName,
                DeliveryPhone = x.DeliveryPhone,
                OrderDate = x.OrderDate,
                CreateDate = x.CreatedDate,
                IsActive = x.IsActive,
                DeliveryFee = x.DeliveryFee,
                Email = x.Email,
                Note = x.Note,
                ToatalPrice = x.ToatalPrice,
                SubPrice = x.SubPrice,
                Status = x.Status
            }).OrderByDescending(p => p.OrderDate).ToList();
        }
    }
}