using S3.Train.WebPerFume.CommonFunction;
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
        public ActionResult ProductDetail(Guid id, string volume)
        {
            var model = GetProductVaDetailViewModel(_productVariationService.GetProductVariationByIdAndVolume_version2(id, volume));

            var product = _productService.GetProductById(id);
            ViewBag.ProductRelate = ConvertDomainToModel.GetProducts(_productService.GetProductsByBrandId(product.Brand_Id).AsQueryable());
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

        private ProductVaDetailViewModel GetProductVaDetailViewModel(ProductVariation productVariation)
        {
            var model = new ProductVaDetailViewModel
            {
               Id = productVariation.Id,
               Price = productVariation.Price,
               DiscountPrice = productVariation.DiscountPrice,
               Product = productVariation.Product,
               ProductImage = productVariation.ProductImage,
               SKU = productVariation.SKU,
               Volume = productVariation.Volume,
               StockQuantity = productVariation.StockQuantity,
               Product_Id = productVariation.Product_Id
            };
            return model;
        }
    }
}