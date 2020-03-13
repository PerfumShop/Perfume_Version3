using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Model.Cart
{
    public class ShoppingCartItemModel
    {
        public string ProductName { get; set; }
        public string Volume { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
