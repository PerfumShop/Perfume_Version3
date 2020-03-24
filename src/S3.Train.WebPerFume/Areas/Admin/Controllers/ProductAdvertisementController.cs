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
    public class ProductAdvertisementController : Controller
    {
        private readonly IProductAdvertisement _productadvertisementService;

        #region Ctor
        public ProductAdvertisementController() { }

        public ProductAdvertisementController(IProductAdvertisement productadvertisementService)
        {
            _productadvertisementService = productadvertisementService;
        }
        #endregion

        #region Index and Detail

        // GET: Admin/ProductAdvertisement
        public ActionResult Index()
        {
            try
            {
                var model = GetProductAdvertisements(_productadvertisementService.SelectAll());
                return View(model);
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        // detail
        public ActionResult DetailProductAdvertisement(Guid id)
        {
            try
            {
                var productadvertisement = _productadvertisementService.GetById(id);
                var model = new ProductAdvertisementViewModel
                {
                    Id = productadvertisement.Id,
                    Title = productadvertisement.Title,
                    EventUrl = productadvertisement.EventUrl,
                    EventUrlCaption = productadvertisement.EventUrlCaption,

                    Description = productadvertisement.Description,
                    ImagePath = productadvertisement.ImagePath,
                    CreateDate = productadvertisement.CreatedDate,
                    IsActive = productadvertisement.IsActive
                };
                return View(model);
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion

        #region Create new or update product advertisement
        [HttpGet]
        public ActionResult AddOrEditProductAdvertisement(Guid? id)
        {
            try
            {
                ProductAdvertisementViewModel model = new ProductAdvertisementViewModel();

                model.DropDownProductAd = DropDownListDomain.DropDownList_ProductADType();

                if (id.HasValue)
                {
                    var productadvertisement = _productadvertisementService.GetById(id.Value);
                    model.Id = productadvertisement.Id;
                    model.Title = productadvertisement.Title;
                    model.EventUrl = productadvertisement.EventUrl;
                    model.EventUrlCaption = productadvertisement.EventUrlCaption;
                    model.Description = productadvertisement.Description;
                    model.ImagePath = productadvertisement.ImagePath;
                    model.CreateDate = productadvertisement.CreatedDate;
                    model.IsActive = productadvertisement.IsActive;
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
        public ActionResult AddOrEditProductAdvertisement(Guid? id, ProductAdvertisementViewModel model, HttpPostedFileBase image)
        {
            try
            {
                bool isNew = !id.HasValue;
                string localFile = "~/Content/img/banner";

                // isNew = true update UpdatedDate of product
                // isNew = false get it by id
                var productadvertisement = isNew ? new ProductAdvertisement
                {
                    UpdatedDate = DateTime.Now
                } : _productadvertisementService.GetById(id.Value);

                productadvertisement.Title = model.Title;
                productadvertisement.EventUrl = model.EventUrl;
                productadvertisement.EventUrlCaption = model.EventUrlCaption;
                productadvertisement.Description = model.Description;
                productadvertisement.ImagePath = _productadvertisementService.UpFile(image, localFile);
                productadvertisement.IsActive = true;
                productadvertisement.AdType = model.productadvertisementType;

                if (isNew)
                {
                    // chage status = false for all Product Advertisement Type same type 
                    var a = _productadvertisementService.GetAllBannerByType(model.productadvertisementType);
                    if (model.productadvertisementType != ProductAdvertisementType.SliderBanner && a.Count() > 1)
                    {
                        foreach (var proVa in a)
                        {
                            _productadvertisementService.ChangeStatus(proVa, false);
                        }
                    }
                    // Add new 
                    productadvertisement.CreatedDate = DateTime.Now;
                    productadvertisement.Id = Guid.NewGuid();
                    _productadvertisementService.Insert(productadvertisement);
                }
                else
                {
                    _productadvertisementService.Update(productadvertisement);
                }
                return RedirectToAction("Index");
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion

        #region change Status 
        /// <summary>
        /// Change status
        /// </summary>
        /// <param name="proVa_id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ActionResult ChangeStatus(Guid proVa_id, bool status)
        {
            try
            {
                var proAd = _productadvertisementService.GetById(proVa_id); // get product advertisement by id

                var list = _productadvertisementService.GetAllBannerByType(proAd.AdType);// get all product advertisement same AdType

                if (status && list.Count() >= 1 && proAd.AdType != ProductAdvertisementType.SliderBanner)
                {
                    // Update all product in list have id != proVa_id to Active = false
                    foreach (var item in list)
                    {
                        if (item.Id != proVa_id)
                            _productadvertisementService.ChangeStatus(item, false);
                    }
                    // update Active product advertisement
                    _productadvertisementService.ChangeStatus(proAd, status);
                }
                if (proAd.AdType == ProductAdvertisementType.SliderBanner)
                {
                    _productadvertisementService.ChangeStatus(proAd, status);
                }
                return RedirectToAction("index");
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion

        #region Delete Product Advertisement
        [HttpGet]
        public ActionResult DeleteProductAdvertisement(Guid id)
        {
            try
            {
                var productadvertisement = _productadvertisementService.GetById(id);
                var model = new ProductAdvertisementViewModel
                {
                    Title = $"{productadvertisement.Title}"
                };
                return PartialView("~/Areas/Admin/Views/ProductAdvertisement/_DeleteProductAdvertisement.cshtml", model);
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        [HttpPost]
        public ActionResult DeleteProductAdvertisement(ProductAdvertisementViewModel model)
        {
            try
            {
                var product = _productadvertisementService.GetById(model.Id);
                _productadvertisementService.Delete(product);
                return RedirectToAction("Index");
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion

        #region Convert domain to model
        /// <summary>
        /// Convert List Product to List ProductViewModel All Properties
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public IList<ProductAdvertisementViewModel> GetProductAdvertisements(IList<ProductAdvertisement> productadvertisements)
        {
            return productadvertisements.Select(x => new ProductAdvertisementViewModel
            {
                Id = x.Id,
                Title= x.Title,
                EventUrl = x.EventUrl,
                EventUrlCaption = x.EventUrlCaption,
                productadvertisementType = x.AdType,
                Description = x.Description,
                ImagePath = x.ImagePath,
                CreateDate = x.CreatedDate,
                IsActive = x.IsActive
            }).ToList();
        }

        public IList<ProductAdvertisementViewModel> GetProductAdvertisement_SummaryInfo(IList<ProductAdvertisement> productadvertisements)
        {
            return productadvertisements.Select(x => new ProductAdvertisementViewModel
            {
                Id = x.Id,
                Title = x.Title,
                EventUrl=x.EventUrl,
                EventUrlCaption=x.EventUrlCaption,
                ImagePath = x.ImagePath,
                CreateDate = x.CreatedDate,
                IsActive = x.IsActive
            }).ToList();
        }
        #endregion

    }
}