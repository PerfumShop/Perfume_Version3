using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S3.Train.WebPerFume.Models
{
    public class HomeViewModel
    {
        public IList<ProductsModel> productsModels  { get; set; }
        public BannerModel BannerMain { get; set; }
        public BannerModel BannerMen { get; set; }
        public BannerModel BannerWomen { get; set; }
        public ProductAd SquareMen { get; set; }
        public ProductAd Squarewomen { get; set; }
        public ProductAd SquareUnisex { get; set; }
        public IList<ProductAd> BannerSlider { get; set; }
    }

    public class BannerModel
    {
        public string Image { get; set; }
  
        public string Link { get; set; }
    }
    public class ProductAd
    {     
        public string EventUrl { get; set; }
      
        public string ImagePath { get; set; }
    }

    public class ProductsModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public string ImagePath { get; set; }
    }
}