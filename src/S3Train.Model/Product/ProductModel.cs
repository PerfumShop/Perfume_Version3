using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Model.Product
{
    public class ProductModel
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
    }
}
