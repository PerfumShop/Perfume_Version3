using S3.Train.WebPerFume.Areas.Admin.Models;
using S3.Train.WebPerFume.CommonFunction;
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

        #region Ctor
        public OrderController() { }

        public OrderController(IOrderService orderService, IShoppingCartService shoppingcartService)
        {
            _orderService = orderService;
            _shoppingcartService = shoppingcartService;
        }
        #endregion

        #region Index And Detail
        // GET: Admin/Order
        public ActionResult Index()
        {
            try
            {
                var model = GetOrders(_orderService.SelectAll());
                return View(model);
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        /// <summary>
        /// get detail Order and display on DetailOrder view
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <returns></returns>
        public ActionResult DetailOrder(Guid id)
        {
            try
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
                ViewBag.OrderStatus = DropDownListDomain.DropDownList_OrderStatus();
                return View(model);
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion

        #region Delete, change status order and convert domain to model
        [HttpPost]
        public ActionResult DeleteOrder(ProductViewModel model)
        {
            try
            {
                var order = _orderService.GetById(model.Id);
                _orderService.Delete(order);
                return RedirectToAction("Index");
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatusOrder(Guid id, string status)
        {
            try
            {
                var order = _orderService.GetById(id);
                var sa = SetOrderStatus.SetStatusTypeInt(Convert.ToInt32(status));
                order.Status = SetOrderStatus.SetStatus(sa);

                _orderService.Update(order);
                return Json(new { id = id, status = status}, JsonRequestBehavior.AllowGet);
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        /// <summary>
        /// Convert List Product to List ProductViewModel All Properties
        /// </summary>
        /// <param name="orders">Orders</param>
        /// <returns>IList Order type OderViewModel</returns>
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
        #endregion
    }
}