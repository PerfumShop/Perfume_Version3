using S3.Train.WebPerFume.Areas.Admin.Models;
using S3.Train.WebPerFume.Models;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S3.Train.WebPerFume.CommonFunction
{
    public static class ConvertDomainToModel
    {
        public static IList<ProductViewModel> GetProduct_SummaryInfo(IList<Product> products)
        {
            return products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ImagePath = x.ImagePath,
                CreateDate = x.CreatedDate,
                IsActive = x.IsActive
            }).ToList();
        }

        public static ProductImageModel GetProductImage(ProductImage productImage)
        {
            var model = new ProductImageModel
            {
                Id = productImage.Id,
                ImagePath = productImage.ImagePath,
                ProducVariation_Id = productImage.ProductVariation_Id,
                CreateDate = productImage.CreatedDate,
                UpdateDate = productImage.UpdatedDate,
                IsActive = productImage.IsActive
            };
            return model;
        }

        public static ProVarationViewModel ConvertModelFromDomainToProVa(ProductVariation productVariation)
        {
            var model = new ProVarationViewModel
            {
                Id = productVariation.Id,
                Product_Id = productVariation.Product_Id,
                SKU = productVariation.SKU,
                Volume = productVariation.Volume,
                Price = productVariation.Price,
                DiscountPrice = productVariation.DiscountPrice,
                StockQuantity = productVariation.StockQuantity,
                CreateDate = productVariation.CreatedDate,
                UpdateDate = productVariation.UpdatedDate,
                IsActive = productVariation.IsActive
            };
            return model;
        }


        /// <summary>
        /// Convert List Product to List ProductViewModel All Properties
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public static IList<ProductsModel> GetProducts(IList<Product> products)
        {
            return products.Select(x => new ProductsModel
            {
                Id = x.Id,
                Name = x.Name,
                ImagePath = x.ImagePath,
                Brand = x.Brand,
                Price = x.ProductVariations.FirstOrDefault(p => p.Product_Id == x.Id).Price,
                DiscountPrice = x.ProductVariations.FirstOrDefault(p => p.Product_Id == x.Id).DiscountPrice,
                Categories = x.Categories,
                ProductVariations = x.ProductVariations
            }).ToList();
        }

        public static IList<ShoppingCartDetailModel> shoppingCartDetailModels(ICollection<ShoppingCartDetail> shoppingCartDetails)
        {
            return shoppingCartDetails.Select(x => new ShoppingCartDetailModel
            {
                Id = x.Id,
                CreatedDate = x.CreatedDate,
                ProductVariation = x.ProductVariation,
                ProductVariation_Id = x.ProductVariation_Id,
                Quantity = x.Quantity,
                ShoppingCart = x.ShoppingCart,
                ShoppingCart_Id = x.ShoppingCart_Id,
                UpdatedDate = x.UpdatedDate,
            }).OrderBy(p => p.CreatedDate).ToList();
        }

    }
}