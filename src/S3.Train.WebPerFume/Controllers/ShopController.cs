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
using System.Web.Mvc;
using X.PagedList;

namespace S3.Train.WebPerFume.Controllers
{
    public class ShopController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IProductService _productService;
        private readonly IProductImageService _productImageService;
        private readonly IProductVariationService _productVariationService;
        public ShopController()
        {

        }

        public ShopController(ICategoryService categoryService, IBrandService brandService, 
            IProductService productService, IProductImageService productImageService, 
            IProductVariationService productVariationService)
        {
            _categoryService = categoryService;
            _brandService = brandService;
            _productService = productService;
            _productImageService = productImageService;
            _productVariationService = productVariationService;
        }

        // GET: Shop
        public ActionResult Index(int? currentPage)
        {
            var model = new ShopViewModel
            {
                brandModels = GetBrandViewModel(),
                categoryModels = GetCategoryViewModel(),
                productModels = GetProductViewModel(currentPage),
                productVarModels = GetProductVarViewModel()
            };

            return View(model);
        }

        private IList<ProductVarModel> GetProductVarViewModel()
        {
            IList<ProductVariation> categories = _productVariationService.SelectAll();
            return categories.Select(x => new ProductVarModel
            {
                Volume = x.Volume
            }).ToList();
        }

        private IPagedList<ProductModel> GetProductViewModel(int? currentPage)
        {
            int pageSize = 6;
            int pageNumber = (currentPage ?? 1);
            IList<Product> products = _productService.SelectAll();
            return products.Select(x => new ProductModel
            {
                Name = x.Name,
                ImagePath = x.ImagePath,
                //some product not have product variations
                Price = 10,//_productVariationService.GetOneProductVariations(x.Id).Price,/
                DiscountPrice = 10,//_productVariationService.GetOneProductVariations(x.Id).DiscountPrice
            }).ToPagedList(pageNumber, pageSize);
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