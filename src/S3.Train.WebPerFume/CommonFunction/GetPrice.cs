using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S3.Train.WebPerFume.CommonFunction
{
    public static class GetPrice
    {
        /// <summary>
        /// Product Price
        /// </summary>
        /// <param name="productVariation"></param>
        /// <returns>price</returns>
        public static decimal GetProductPrice(ProductVariation productVariation)
        {
            if (productVariation.DiscountPrice > 0 && productVariation.DiscountPrice != null)
                return Convert.ToDecimal(productVariation.DiscountPrice);
            else
                return productVariation.Price;
        }
    }
}