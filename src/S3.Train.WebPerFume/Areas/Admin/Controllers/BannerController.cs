using S3.Train.WebPerFume.Areas.Admin.Models;
using S3.Train.WebPerFume.CommonFunction;
using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S3.Train.WebPerFume.Areas.Admin.Controllers
{
    [Authorize(Users = "Admin")]
    public class BannerController : Controller
    {
        // GET: Admin/Banner
        private readonly IBannerService _bannerService;

        #region Ctor
        public BannerController() { }

        public BannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
          
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            try
            {
                var model = GetBanners(_bannerService.SelectAll());
                return View(model);
            }
            catch { return RedirectToAction("Erorr500","HomdeAdmin"); }
        }
        #endregion

        #region Create or update Banner
        [HttpGet]
        public ActionResult AddOrEditBanner(Guid? id)
        {
            try
            {
                BannerViewModel model = new BannerViewModel();

                model.DropDownBannerType = DropDownListDomain.DropDownList_BannerType();

                if (id.HasValue)
                {
                    var banner = _bannerService.GetById(id.Value);
                    model.Id = banner.Id;
                    model.Image = banner.Image;
                    model.Link = banner.Link;

                    model.CreateDate = banner.CreatedDate;
                    model.IsActive = banner.IsActive;
                    return View(model);
                }
                else
                    return View(model);
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        /// <summary>
        /// If id != null Update else Create new
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="model">ProductViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddOrEditBanner(Guid? id, BannerViewModel model, HttpPostedFileBase image)
        {
            try
            {
                bool isNew = !id.HasValue;
                string localFile = "~/Content/img/banner";

                // isNew = true update UpdatedDate of product
                // isNew = false get it by id
                var banner = isNew ? new Banner
                {
                    UpdatedDate = DateTime.Now
                } : _bannerService.GetById(id.Value);
                banner.Image = _bannerService.UpFile(image, localFile);
                banner.Link = model.Link;
                banner.IsActive = true;
                banner.AdType = model.bannerType;
                if (isNew)
                {
                    // chage status = false for all Product Advertisement Type same type 
                    foreach (var proVa in _bannerService.GetAllBannerSameType(model.bannerType))
                    {
                        _bannerService.ChangeStatus(proVa, false);
                    }
                    banner.CreatedDate = DateTime.Now;
                    banner.Id = Guid.NewGuid();
                    _bannerService.Insert(banner);
                }
                else
                {
                    _bannerService.Update(banner);
                }
                return RedirectToAction("Index");
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion

        #region Delete Banner      
        [HttpPost]
        public ActionResult DeleteBanner(BannerViewModel model)
        {
            try
            {
                var banner = _bannerService.GetById(model.Id);
                _bannerService.Delete(banner);
                return RedirectToAction("Index");
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion

        #region Change Status Banner
        public ActionResult ChangeStatus(Guid banner_Id, bool status)
        {
            try
            {
                var banner = _bannerService.GetById(banner_Id); 

                var listBannerSameType = _bannerService.GetAllBannerSameType(banner.AdType);

                if (status && listBannerSameType.Count() >= 1)
                {
                    foreach (var item in listBannerSameType)
                    {
                        if (item.Id != banner_Id)
                            _bannerService.ChangeStatus(item, false);
                    }
                    _bannerService.ChangeStatus(banner, status);
                }

                return RedirectToAction("index");
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion

        private IList<BannerViewModel> GetBanners(IList<Banner> banners)
        {
            return banners.Select(x => new BannerViewModel
            {
                Id = x.Id,
                Link = x.Link,
                bannerType = x.AdType,
                Image = x.Image,
                CreateDate = x.CreatedDate,
                IsActive = x.IsActive
            }).OrderByDescending(p=>p.CreateDate).ToList();
        }
    }
}