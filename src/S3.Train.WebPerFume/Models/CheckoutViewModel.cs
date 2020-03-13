using S3Train.Model.Cart;
using S3Train.Model.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S3.Train.WebPerFume.Models
{
    public class CheckoutViewModel
    {
        public CustomerModel customerModel { get; set; }
        public IList<ShoppingCartItemModel> shoppingCartItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}