using S3.Train.WebPerFume.Models;
using S3Train.Domain;
using S3Train.Model.Cart;
using S3Train.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S3.Train.WebPerFume.Services
{
    public class CheckoutService /*: ICheckoutService*/
    {
        private readonly ShoppingCartService _shoppingCartService;
        private readonly ShoppingCartDetailService _shoppingCartDetailService;
        private readonly OrderService _orderService;

        public CheckoutService(ShoppingCartService shoppingCartService, 
            ShoppingCartDetailService shoppingCartDetailService, OrderService orderService)
        {
            _shoppingCartService = shoppingCartService;
            _shoppingCartDetailService = shoppingCartDetailService;
            _orderService = orderService;
        }

        //public CheckoutViewModel GetCheckoutModel(object cookie)
        //{
        //    CheckoutViewModel result = new CheckoutViewModel();
        //    IList<ShoppingCartDetail> shoppingCartDetails =_shoppingCartService.GetCartDetailsByUserId(cookie.ToString);
        //    result.shoppingCartItems = shoppingCartDetails.Select(x => new ShoppingCartItemModel
        //    {
        //        ProductName = x.ProductVariation.Product.Name,
        //        Quantity = x.Quantity,
        //        TotalPrice = _shoppingCartDetailService.GetTotalPriceItem(x.Id),
        //        Volume = x.ProductVariation.Volume
        //    }).ToList();
        //    result.SubTotal = _shoppingCartService.GetSubTotalPrice(cookie.ToString);
        //    result.Total = _shoppingCartService.GetTotalPrice(cookie.ToString);
        //    return result;
        //}

        //public bool SaveOrder(CheckoutViewModel model)
        //{
        //    var result = true;
        //    try
        //    {
        //        Order order = new Order
        //        {
        //            Id = new Guid(),
        //            CreatedDate = DateTime.Now,
        //            ShoppingCart_Id = _shoppingCartService.GetIdByUserId(),
        //            DeliveryName = model.customerModel.LastName + " " + model.customerModel.FirstName,
        //            DeliveryAddress = model.customerModel.Street + " "+ 
        //            model.customerModel.Town +" "+ model.customerModel.Country,
        //            DeliveryPhone = model.customerModel.Phone
        //        };
        //        _orderService.Insert(order);
        //    }
        //    catch { result = false; }
        //    return result;
        //}
    }
}