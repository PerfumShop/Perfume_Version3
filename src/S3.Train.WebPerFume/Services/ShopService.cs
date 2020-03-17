using S3.Train.WebPerFume.Models;
using S3Train.Contract;
using S3Train.Domain;
using S3Train.Model.Brand;
using S3Train.Model.Category;
using S3Train.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S3.Train.WebPerFume.Services
{
    public class ShopService : IShopService
    {
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IProductListViewService _productListViewService;

        public ShopService(ICategoryService categoryService, IBrandService brandService, 
            IProductListViewService productListViewService)
        {
            _categoryService = categoryService;
            _brandService = brandService;
            _productListViewService = productListViewService;
        }

        public ShopViewModel GetShopViewModel(int? currentPage, string searchFilter, string searchValue, string sortOrder)
        {
            ShopViewModel result = new ShopViewModel()
            {
                brandModels = GetBrandViewModel(),
                categoryModels = GetCategoryViewModel(),
                productListModels = _productListViewService.GetProductListViewModel(currentPage, searchFilter, searchValue, sortOrder),
            };
            return result;
        }
        private IList<CategoryModel> GetCategoryViewModel()
        {
            IList<Category> categories = _categoryService.SelectAll();
            return categories.Select(x => new CategoryModel
            {
                Name = x.Name
            }).ToList();
        }

        private IList<BrandModel> GetBrandViewModel()
        {
            IList<Brand> brands = _brandService.SelectAll();
            return brands.Select(x => new BrandModel
            {
                Name = x.Name
            }).ToList();
        }
    }
}