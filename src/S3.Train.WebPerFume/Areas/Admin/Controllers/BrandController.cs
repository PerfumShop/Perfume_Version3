﻿using S3.Train.WebPerFume.Areas.Admin.Models;
using S3.Train.WebPerFume.CommonFunction;
using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S3.Train.WebPerFume.Areas.Admin.Controllers
{
    [Authorize(Users = "Admin")]
    public class BrandController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;

        #region Ctor
        public BrandController()
        {

        }

        public BrandController(IProductService productService, IBrandService brandService)
        {
            _productService = productService;
            _brandService = brandService;
            _productService = productService;
            _brandService = brandService;
        }
        #endregion

        #region Index and Detail Brand
        // GET: Admin/Brand
        public ActionResult Index()
        {
            try
            {
                var model = GetBrands(_brandService.SelectAll());
                return View(model);
            }
            catch { return RedirectToAction("Erorr500","HomdeAdmin"); }
        }

        /// <summary>
        /// Detail Brand
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Detail View</returns>
        public ActionResult DetailBrand(Guid id)
        {
            try
            {
                var brand = _brandService.GetById(id);
                var model = new BrandViewModel
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    Logo = brand.Logo,
                    CreateDate = brand.CreatedDate,
                    IsActive = brand.IsActive,
                    Summary = brand.Summary,
                    Products = ConvertDomainToModel.GetProduct_SummaryInfo(_productService.GetProductsByBrandId(brand.Id)),
                    UpdateDate = brand.UpdatedDate,
                };
                return View(model);
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion

        #region Create new or update Brand
        [HttpGet]
        public ActionResult AddOrEditBrand(Guid? id)
        {
            try
            {
                BrandViewModel model = new BrandViewModel();

                if (id.HasValue)
                {
                    var brand = _brandService.GetById(id.Value);
                    model.Id = brand.Id;
                    model.Name = brand.Name;
                    model.Logo = brand.Logo;
                    model.Summary = brand.Summary;
                    model.UpdateDate = brand.UpdatedDate;
                    model.CreateDate = brand.CreatedDate;
                    model.IsActive = brand.IsActive;
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
        public ActionResult AddOrEditBrand(Guid? id, BrandViewModel model, HttpPostedFileBase logo)
        {
            try
            {
                bool isNew = !id.HasValue;
                string localFile = Server.MapPath("~/Content/img/logocarousel/");

                // isNew = true update UpdatedDate of product
                // isNew = false get it by id
                var brand = isNew ? new Brand
                {
                    UpdatedDate = DateTime.Now
                } : _brandService.GetById(id.Value);

                brand.Name = model.Name;
                brand.Summary = model.Summary;
                brand.Logo = _brandService.UpFile(logo, localFile);
                brand.IsActive = true;

                if (isNew)
                {
                    brand.CreatedDate = DateTime.Now;
                    brand.Id = Guid.NewGuid();
                    _brandService.Insert(brand);
                }
                else
                {
                    _brandService.Update(brand);
                }
                return RedirectToAction("Index");
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion

        #region Delete Brand
        [HttpPost]
        public ActionResult DeleteBrand(BrandViewModel model)
        {
            try
            {
                var brand = _brandService.GetById(model.Id);
                _brandService.Delete(brand);
                return RedirectToAction("Index");
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion

        #region Change status Brand and Convert domain to model
        /// <summary>
        /// change status brand
        /// </summary>
        /// <param name="brand_Id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ActionResult ChangeStatusBrand(Guid brand_Id, bool status)
        {
            try
            {
                var brand = _brandService.GetById(brand_Id);
                _brandService.ChangeStatus(brand, status);
                return RedirectToAction("DetailBrand", new { id = brand_Id });
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        private IList<BrandViewModel> GetBrands(IList<Brand> brands)
        {
            return brands.Select(x => new BrandViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Summary = x.Summary,
                Logo = x.Logo,
                CreateDate = x.CreatedDate,
                UpdateDate = x.UpdatedDate,
                Products = ConvertDomainToModel.GetProduct_SummaryInfo(_productService.GetProductsByBrandId(x.Id)),
                IsActive = x.IsActive
            }).ToList();
        }
        #endregion
    }
}