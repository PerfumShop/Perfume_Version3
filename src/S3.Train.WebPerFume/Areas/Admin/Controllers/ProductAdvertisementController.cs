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
    public class ProductAdvertisementController : Controller
    {
        private readonly IProductAdvertisement _productadvertisementService;
       

        public ProductAdvertisementController() { }

        public ProductAdvertisementController(IProductAdvertisement productadvertisementService)
        {
            _productadvertisementService = productadvertisementService;
           

        }

        // GET: Admin/ProductAdvertisement
        public ActionResult Index()
        {
            var model = GetProductAdvertisements(_productadvertisementService.SelectAll());
            return View(model);
        }

        public ActionResult DetailProductAdvertisement(Guid id)
        {
            var productadvertisement = _productadvertisementService.GetById(id);
            var model = new ProductAdvertisementViewModel
            {
                Id = productadvertisement.Id,
                Title = productadvertisement.Title,
                EventUrl= productadvertisement.EventUrl,
                EventUrlCaption= productadvertisement.EventUrlCaption,

                Description = productadvertisement.Description,
                ImagePath = productadvertisement.ImagePath,
                CreateDate = productadvertisement.CreatedDate,
                IsActive = productadvertisement.IsActive
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult AddOrEditProductAdvertisement(Guid? id)
        {
            ProductAdvertisementViewModel model = new ProductAdvertisementViewModel();

            model.DropDownProductAd = DropDownListDomain.DropDownList_ProductADType();

            if (id.HasValue)
            {
                var productadvertisement = _productadvertisementService.GetById(id.Value);
                model.Id = productadvertisement.Id;
                model.Title = productadvertisement.Title;
                model.EventUrl= productadvertisement.EventUrl;
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
                productadvertisement.ImagePath = UpFile(image, localFile);
                productadvertisement.IsActive = true;
                productadvertisement.AdType = model.productadvertisementType;

                if (isNew)
                {
                    // chage status = false for all Product Advertisement Type same type 
                    if (model.productadvertisementType != ProductAdvertisementType.SliderBanner)
                    {
                        foreach (var proVa in _productadvertisementService.GetAllBannerByType(model.productadvertisementType))
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
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Change status
        /// </summary>
        /// <param name="proVa_id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ActionResult ChangeStatus(Guid proVa_id, bool status)
        {
            var productAdvertisement = _productadvertisementService.GetById(proVa_id);

            var listProVaSameType = _productadvertisementService.GetAllBannerByType(productAdvertisement.AdType);

            var count = listProVaSameType.Where(m => m.IsActive == true).Count();

            _productadvertisementService.ChangeStatus(productAdvertisement, status);
            if(count <= 1)
            {
                _productadvertisementService.ChangeStatus(productAdvertisement, !status);
            }
            return RedirectToAction("index");
        }

        [HttpGet]
        public PartialViewResult DeleteProductAdvertisement(Guid id)
        {
            var productadvertisement = _productadvertisementService.GetById(id);
            var model = new ProductAdvertisementViewModel
            {
                Title = $"{productadvertisement.Title}"
            };
            return PartialView("~/Areas/Admin/Views/ProductAdvertisement/_DeleteProductAdvertisement.cshtml", model);
        }

        [HttpPost]
        public ActionResult DeleteProductAdvertisement(ProductAdvertisementViewModel model)
        {
            var product = _productadvertisementService.GetById(model.Id);
            _productadvertisementService.Delete(product);
            return RedirectToAction("Index");
        }

    

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


        /// <summary>
        /// Upload file and save in folder
        /// </summary>
        /// <param name="a">choose file</param>
        /// <param name="url">local save file </param>
        /// <returns>file name</returns>
        public string UpFile(HttpPostedFileBase a, string url)
        {
            string fileName = "";
            if (a != null && a.ContentLength > 0)
            {
                fileName = Path.GetFileName(a.FileName).ToString();
                string path = Path.Combine(Server.MapPath(url), fileName);
                a.SaveAs(path);
                return fileName;
            }
            else
            {
                return fileName;
            }
        }
    }
}