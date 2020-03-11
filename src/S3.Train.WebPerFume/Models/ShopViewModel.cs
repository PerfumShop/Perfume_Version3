using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using S3Train.Domain;
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

    public class ProductDetailModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ProductVariation ProVariation { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<ProductVariation> ProductVariations { get; set; }
    }

    public class ProductVaDetailViewModel
    {
        public Guid Id { get; set; }
        public Guid Product_Id { get; set; }
        public string SKU { get; set; }
        public string Volume { get; set; }
        public decimal StockQuantity { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
        public virtual ICollection<ShoppingCartDetail> ShoppingCartDetails { get; set; }
    }
}