using S3.Train.WebPerFume.Areas.Admin.Models;
using S3Train.Contract;
using S3Train.Domain;
using S3Train.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S3.Train.WebPerFume.Areas.Admin.Controllers
{
    [Authorize(Users = "Admin")]
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
            var order = _orderService.GetById(id);
            var model = new OrderViewModel
            {
                Id = order.Id,
                DeliveryAddress = order.DeliveryAddress,
                DeliveryName = order.DeliveryName,
                DeliveryPhone = order.DeliveryPhone,
                OrderDate = order.OrderDate,
                ShoppingCart = _shoppingcartService.GetById(order.ShoppingCart_Id),
                CreateDate = order.CreatedDate,
                IsActive = order.IsActive
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult AddOrEditOrder(Guid? id)
        {
            OrderViewModel model = new OrderViewModel();

            if (id.HasValue)
            {
                var order = _orderService.GetById(id.Value);
                model.Id = order.Id;
                model.DeliveryName = order.DeliveryName;
                model.DeliveryAddress = order.DeliveryAddress;
                model.DeliveryPhone = order.DeliveryPhone;
                model.OrderDate = order.OrderDate;
                model.CreateDate = order.CreatedDate;
                model.IsActive = order.IsActive;
                return View(model);
            }
            else
                return View(model);
        }

        /// <summary>
        /// If id != null Update else Create new
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="model">ProductViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddOrEditOrder(Guid? id, OrderViewModel model, HttpPostedFileBase image)
        {
            try
            {
                bool isNew = !id.HasValue;

                // isNew = true update UpdatedDate of product
                // isNew = false get it by id
                var order = isNew ? new Order
                {
                    UpdatedDate = DateTime.Now
                } : _orderService.GetById(id.Value);


                order.DeliveryName = model.DeliveryName;
                order.DeliveryAddress = model.DeliveryAddress;
                order.DeliveryPhone = model.DeliveryPhone;
                order.OrderDate = DateTime.Now;
                order.ShoppingCart_Id = model.ShoppingCart_Id;
                order.IsActive = false;

                if (isNew)
                {
                    order.CreatedDate = DateTime.Now;
                    order.Id = Guid.NewGuid();
                    _orderService.Insert(order);
                }
                else
                {
                    _orderService.Update(order);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
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
                ShoppingCart_Id = x.ShoppingCart_Id,
                CreateDate = x.CreatedDate,
                IsActive = x.IsActive
            }).OrderByDescending(p => p.OrderDate).ToList();
        }



        /// <summary>
        /// Upload file and save in folder
        /// </summary>
        /// <param name="a">choose file</param>
        /// <param name="url">local save file </param>
        /// <returns>file name</returns>

    }
}