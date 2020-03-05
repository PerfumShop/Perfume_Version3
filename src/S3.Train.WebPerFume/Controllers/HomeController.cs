using S3.Train.WebPerFume.Models;
using S3Train.Contract;
using S3Train.Domain;
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

        public HomeController()
        {

        }

        public HomeController(IBannerService bannerService,IProductAdvertisement productAdvertisement,
            IProductVariationService productVariationService, IProductService productService,
            IProductImageService productImageService)
        {
            _bannerService = bannerService;
            _productAdvertisement = productAdvertisement;
            _productVariationService = productVariationService;
            _productService = productService;
            _productImageService = productImageService;
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

        private IList<ProductsModel> GetProducts(IList<ProductVariation> products)
        {
            return products.Select(x => new ProductsModel
            {
               Id= x.Id,
               Name = _productService.GetById(x.Product_Id).Name,
               Price=x.Price,
               DiscountPrice=x.DiscountPrice,
               ImagePath=_productImageService.GetProductImage(x.Id).ImagePath,
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

        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Checkout()
        { return View(); }
        public ActionResult Shop()
        { return View(); }
    }
}