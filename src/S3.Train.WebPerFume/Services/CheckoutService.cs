using S3.Train.WebPerFume.Models;
using S3Train.Contract;
using S3Train.Domain;
using S3Train.Model.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S3.Train.WebPerFume.Services
{
    public class CheckoutService /*: ICheckoutService*/
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IShoppingCartDetailService _shoppingCartDetailService;
        private readonly IOrderService _orderService;

        public CheckoutService(IShoppingCartService shoppingCartService, 
            IShoppingCartDetailService shoppingCartDetailService, IOrderService orderService)
        {
            _shoppingCartService = shoppingCartService;
            _shoppingCartDetailService = shoppingCartDetailService;
            _orderService = orderService;
        }

        //public CheckoutViewModel GetCheckoutModel(string userId)
        //{
        //    CheckoutViewModel result = new CheckoutViewModel();
        //    ShoppingCart shoppingCart = _shoppingCartService.GetShoppingCartByUserId(userId);
        //    IList<ShoppingCartDetail> shoppingCartDetails = shoppingCart.ShoppingCartDetails.ToList();
        //    result.shoppingCartItems = shoppingCartDetails.Select(x => new ShoppingCartItemModel
        //    {
        //        ProductName = x.ProductVariation.Product.Name,
        //        Quantity = x.Quantity,
        //        TotalPrice = _shoppingCartDetailService.GetTotalPriceItem(x.Id),
        //        Volume = x.ProductVariation.Volume
        //    }).ToList();
        //    result.SubTotal = shoppingCart.TotalPrice;
        //    result.Total = shoppingCart.TotalPrice;
        //    return result;
        //}

        //public bool SaveOrder(CheckoutViewModel model, string userId)
        //{
        //    var result = true;
        //    try
        //    {
        //        Order order = new Order
        //        {
        //            Id = new Guid(),
        //           CreatedDate = DateTime.Now,
        //            ShoppingCart_Id = _shoppingCartService.GetShoppingCartByUserId(userId).Id,
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