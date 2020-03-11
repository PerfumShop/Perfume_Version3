using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using S3Train.Model.Brand;
using S3Train.Model.Category;
using S3Train.Model.Product;
using System.Web.Mvc;
using X.PagedList;

namespace S3.Train.WebPerFume.Models
{
    public class ShopViewModel
    {
        public IList<CategoryModel> categoryModels { get; set; }
        public ProductListModel productListModels { get; set; }
        public IList<BrandModel> brandModels { get; set; }
        public IList<ProductVarModel> productVarModels { get; set; }
    }
}