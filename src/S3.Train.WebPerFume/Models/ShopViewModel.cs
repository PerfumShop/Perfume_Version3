using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using S3Train.Model.Brand;
using S3Train.Model.Category;
using S3Train.Model.Product;

namespace S3.Train.WebPerFume.Models
{
    public class ShopViewModel
    {
        public IList<CategoryModel> categoryModels;
        public IList<ProductModel> productModels;
        public IList<BrandModel> brandModels;
        public IList<ProductVarModel> productVarModels;
    }
}