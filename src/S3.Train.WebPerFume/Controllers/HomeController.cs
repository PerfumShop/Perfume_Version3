using S3.Train.WebPerFume.CommonFunction;
using S3.Train.WebPerFume.Models;
using S3Train.Contract;
using S3Train.Core.Constant;
using S3Train.Domain;
using S3Train.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

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

        public ActionResult Erorr()
        {
            return View();
        }

        /// <summary>
        /// function search
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult SearchProduct(string SearchText, int? pageIndex)
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                int pageSize = 20;
                var page = pageIndex ?? 1;

                var result = ConvertDomainToModel.GetProducts(_productService.ManySearch(SearchText));
                
                ViewBag.SearchText = SearchText;
                return View(result.ToPagedList(page, pageSize));
            }
            else
            {
                ViewBag.Error = "Not Empty";
                return RedirectToAction("index");
            }
            
        }
    }
}