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
        public ActionResult Index()
        {
            var model = new ShopViewModel
            {
                brandModels = GetBrandViewModel(),
                categoryModels = GetCategoryViewModel(),
                productModels = GetProductViewModel(),
                productVarModels = GetProductVarViewModel()
            };

            return View(model);
        }

        /// <summary>
        /// Product Detail
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>View with model info product</returns>
        public ActionResult ProductDetail(Guid id)
        {
            var model = GetProductDetailModel(_productService.GetProductById(id));
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

        private IList<ProductModel> GetProductViewModel()
        {
            IList<Product> products = _productService.SelectAll();
            return products.Select(x => new ProductModel
            {
                Name = x.Name,
                ImagePath = x.ImagePath,
                //some product not have product variations
                Price = 10,//_productVariationService.GetOneProductVariations(x.Id).Price,/
                DiscountPrice = 10,//_productVariationService.GetOneProductVariations(x.Id).DiscountPrice
            }).ToList();
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


        /// <summary>
        /// Convert from product to ProductDetailModel
        /// </summary>
        /// <param name="product">product</param>
        /// <returns>Product Detail Model</returns>
        private ProductDetailModel GetProductDetailModel(Product product)
        {
            var model = new ProductDetailModel
            {
                Id = product.Id,
                Brand = product.Brand,
                Categories =  product.Categories,
                Description = product.Description,
                ImagePath =  product.ImagePath,
                Name =  product.Name,
                ProductVariations = product.ProductVariations,
                Vendor = product.Vendor
            };
            return model;
        }
    }
}