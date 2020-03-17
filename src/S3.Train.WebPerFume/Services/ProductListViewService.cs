using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using S3Train.Contract;
using S3Train.Domain;
using S3Train.Model.Product;
using X.PagedList;

namespace S3.Train.WebPerFume.Services
{
    public class ProductListViewService : IProductListViewService
    {
        private readonly IProductService _productService;

        public ProductListViewService(IProductService productService)
        {
            _productService = productService;
        }

        public ProductListModel GetProductListViewModel(int? currentPage, string searchFilter, string searchValue, string sortOrder)
        {
            ProductListModel result = new ProductListModel();
            int pageSize = 9;
            int pageNumber = (currentPage ?? 1);
            IList<Product> products = GetProducts(currentPage, searchFilter, searchValue, sortOrder).ToList();
            result.productModels = products.Select(x => new ProductModel
            {
                Name = x.Name,
                ImagePath = x.ImagePath,
                //some product have no product variations
                Price = 10,//_productVariationService.GetOneProductVariations(x.Id).Price,/
                DiscountPrice = 10,//_productVariationService.GetOneProductVariations(x.Id).DiscountPrice
            }).ToPagedList(pageNumber, pageSize);
            return result;
        }
        public IQueryable<Product> GetProducts(int? currentPage, string searchFilter, string searchValue, string sortOrder)
        {
            IQueryable<Product> products = _productService.Gets();
            if (!String.IsNullOrEmpty(searchFilter))
            {
                switch (searchFilter)
                {
                    case "volume":
                        products = _productService.Gets(f => f.ProductVariations.FirstOrDefault().Volume == searchValue);
                        break;
                    case "category":
                        products = _productService.Gets(f => f.Categories.FirstOrDefault().Name == searchValue);
                        break;
                }
            }
            switch (sortOrder)
            {
                case "name":
                    products = products.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "price":
                    products = products.OrderBy(s => s.ProductVariations.FirstOrDefault().Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(s => s.ProductVariations.FirstOrDefault().Price);
                    break;
                case "category":
                    products = products.OrderBy(s => s.Categories.FirstOrDefault().Name);
                    break;
                case "category_desc":
                    products = products.OrderByDescending(s => s.Categories.FirstOrDefault().Name);
                    break;
                case "brand":
                    products = products.OrderBy(s => s.Brand.Name);
                    break;
                case "brand_desc":
                    products = products.OrderByDescending(s => s.Brand.Name);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }
            return products;
            //return products.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}