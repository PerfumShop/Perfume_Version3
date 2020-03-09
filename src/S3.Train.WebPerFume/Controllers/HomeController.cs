using S3.Train.WebPerFume.Models;
using S3Train.Contract;
using S3Train.Domain;
using S3Train.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S3.Train.WebPerFume.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBannerService _bannerService;
        private readonly IProductAdvertisement _productAdvertisement;
        private readonly IProductVariationService  _productVariationService;
        private readonly IProductService  _productService;
        private readonly IProductImageService _productImageService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        public HomeController()
        {

        }

        public HomeController(IBannerService bannerService,IProductAdvertisement productAdvertisement,
            IProductVariationService productVariationService, IProductService productService,
            IProductImageService productImageService, ICategoryService categoryService, IBrandService brandService)
        {
            _bannerService = bannerService;
            _productAdvertisement = productAdvertisement;
            _productVariationService = productVariationService;
            _productService = productService;
            _productImageService = productImageService;
            _categoryService = categoryService;
            _brandService = brandService;
        }

        public ActionResult Index()
        {
            var model = new HomeViewModel();

            model.BannerMain = GetBanner(_bannerService.GetMainBanner());
            model.BannerMen = GetBanner(_bannerService.GetMenBanner());
            model.BannerWomen = GetBanner(_bannerService.GetWomenBanner());
            model.SquareMen = GetProAd(_productAdvertisement.GetMenSquareBanner());
            model.Squarewomen = GetProAd(_productAdvertisement.GetWomenSquareBanner());
            model.SquareUnisex = GetProAd(_productAdvertisement.GetUnisexSquareBanner());
            //model.productsModels = GetProducts(_productVariationService.SelectAll());

            model.BannerSlider = GetAllSliderBanner();

            return View(model);
        }

        private IList<ProductAd> GetAllSliderBanner()
        {
            IList<ProductAdvertisement> AllSliderBanner = _productAdvertisement.GetAllBannerByType(ProductAdvertisementType.SliderBanner);
            return AllSliderBanner.Select(x => new ProductAd
            {
                ImagePath = x.ImagePath,
                EventUrl = x.EventUrl
            }).ToList();
        }


        private IList<BannerModel> GetBanners(IList<BannerModel> banners)
        {
            return banners.Select(x => new BannerModel
            {
                Link = x.Link,
                Image = x.Image,
            }).ToList();
        }

        private BannerModel GetBanner(Banner banners)
        {
            var model = new BannerModel
            {
                Image = banners.Image,
                Link = banners.Link
            };

            return model;
        }
        private ProductAd GetProAd(ProductAdvertisement productad)
        {
            var pr = new ProductAd
            {
                ImagePath = productad.ImagePath,
                EventUrl = productad.EventUrl
            };

            return pr;
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        /// <summary>
        /// function search
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchProduct(SearchViewModel model)
        {
            if (!string.IsNullOrEmpty(model.SearchText))
            {
                var result = GetProducts(_productService.ManySearch(model));
                ViewBag.SearchText = model.SearchText;
                return View(result);
            }
            else
            {
                ViewBag.Error = "You";
                return RedirectToAction("index");
            }
            
        }

        public ActionResult Checkout()
        { return View(); }
        public ActionResult Shop()
        { return View(); }



        /// <summary>
        /// Convert List Product to List ProductViewModel All Properties
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public IQueryable<ProductsModel> GetProducts(IQueryable<Product> products)
        {
            return products.Select(x => new ProductsModel
            {
                Id = x.Id,
                Name = x.Name,
                ImagePath = x.ImagePath,
                Brand = x.Brand,
                Price = x.ProductVariations.FirstOrDefault(p=> p.Product_Id == x.Id).Price,
                DiscountPrice = x.ProductVariations.FirstOrDefault(p => p.Product_Id == x.Id).DiscountPrice,
                Categories = x.Categories,
                ProductVariations = x.ProductVariations
            }).AsQueryable();
        }
        public ActionResult ProductDetail()
        { return View(); }
        public ActionResult Shoppingcart()
        { return View(); }
    }
}