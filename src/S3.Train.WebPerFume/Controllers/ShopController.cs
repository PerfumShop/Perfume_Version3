using S3.Train.WebPerFume.CommonFunction;
using S3.Train.WebPerFume.Models;
using S3.Train.WebPerFume.Services;
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
        private readonly IProductService _productService;
        private readonly IProductVariationService _productVariationService;
        private readonly IShopService _shopService;
        private readonly IProductListViewService _productListViewService;

        public ShopController(IProductService productService, IProductVariationService productVariationService, 
            IShopService shopService, IProductListViewService productListViewService)
        {
            _productService = productService;
            _productVariationService = productVariationService;
            _shopService = shopService;
            _productListViewService = productListViewService;
        }

        // GET: Shop
        public ActionResult Index(string sortOrder,int? currentPage, string currentFilter, string currentFilterValue, string searchValue, string searchFilter)
        {
            ViewBag.CurrentSort = sortOrder;
            if (searchFilter != null)
            {
                currentPage = 1;
            }
            else
            {
                searchFilter = currentFilter;
                searchValue = currentFilterValue;
            }
            ViewBag.CurrentFilter = searchFilter;
            ViewBag.CurrentFilterValue = searchValue;
            var model = _shopService.GetShopViewModel(currentPage, searchFilter, searchValue, sortOrder);
            return View(model);
        }
        public ActionResult ProductList(string sortOrder, int? currentPage,string currentFilter, string currentFilterValue, string searchValue, string searchFilter)
        {
            ViewBag.CurrentSort = sortOrder;
            if (searchFilter != null)
            {
                currentPage = 1;
            }
            else
            {
                searchFilter = currentFilter;
                searchValue = currentFilterValue;
            }
            ViewBag.CurrentFilter = searchFilter;
            ViewBag.CurrentFilterValue = searchValue;
            var model = _productListViewService.GetProductListViewModel(currentPage, searchFilter, searchValue, sortOrder);
            return PartialView("/Views/Partials/ProductList.cshtml",model);
        }
        /// <summary>
        /// Product Detail
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>View with model info product</returns>
        public ActionResult ProductDetail(Guid id, string volume)
        {
            string vo;
            // get volume
            if(volume==null)
                vo = _productVariationService.GetVolumeFisrtById(id);
            else
                vo = volume;

            var model = GetProductVaDetailViewModel(_productVariationService.GetProductVariationByIdAndVolume_version2(id, vo));

            var product = _productService.GetProductById(id);
            ViewBag.ProductRelate = ConvertDomainToModel.GetProducts(_productService.GetProductsByBrandId(product.Brand_Id));
            return View(model);
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