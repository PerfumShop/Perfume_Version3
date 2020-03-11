using S3.Train.WebPerFume.Areas.Admin.Models;
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
    public class VendorController : Controller
    {
        private readonly IProductService _productService;
        private readonly IVendorService _vendorService;

        public VendorController()
        {

        }

        public VendorController(IProductService productService, IVendorService vendorService)
        {
            _productService = productService;
            _vendorService = vendorService;
        }

        // GET: Admin/Brand
        public ActionResult Index()
        {
            var model = GetVendors(_vendorService.SelectAll());
            return View(model);
        }


        /// <summary>
        /// Detail Brand
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Detail View</returns>
        public ActionResult DetailVendor(Guid id)
        {
            var vendor = _vendorService.GetById(id);
            var model = new VendorViewModel
            {
                Id = vendor.Id,
                Name = vendor.Name,
                Address = vendor.Address,
                CreateDate = vendor.CreatedDate,
                IsActive = vendor.IsActive,
                Email = vendor.Email,
                PhoneNumber= vendor.PhoneNumber,
                Products = ConvertDomainToModel.GetProduct_SummaryInfo(_productService.GetProductsByVendorId(vendor.Id)),
                UpdateDate = vendor.UpdatedDate,
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult AddOrEditVendor(Guid? id)
        {
            VendorViewModel model = new VendorViewModel();

            if (id.HasValue)
            {
                var vendor = _vendorService.GetById(id.Value);
                model.Id = vendor.Id;
                model.Name = vendor.Name;
                model.Address = vendor.Address;
                model.Email = vendor.Email;
                model.PhoneNumber = vendor.PhoneNumber;
                model.UpdateDate = vendor.UpdatedDate;
                model.CreateDate = vendor.CreatedDate;
                model.IsActive = vendor.IsActive;
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
        public ActionResult AddOrEditVendor(Guid? id, VendorViewModel model, HttpPostedFileBase logo)
        {
            try
            {
                bool isNew = !id.HasValue;

                // isNew = true update UpdatedDate of product
                // isNew = false get it by id
                var vendor = isNew ? new Vendor
                {
                    UpdatedDate = DateTime.Now
                } : _vendorService.GetById(id.Value);

                vendor.Name = model.Name;
                vendor.Address = model.Address;
                vendor.Email = model.Email;
                vendor.PhoneNumber = model.PhoneNumber;
                vendor.IsActive = true;

                if (isNew)
                {
                    vendor.CreatedDate = DateTime.Now;
                    vendor.Id = Guid.NewGuid();
                    _vendorService.Insert(vendor);
                }
                else
                {
                    _vendorService.Update(vendor);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }



        [HttpGet]
        public PartialViewResult DeleteVendor(Guid id)
        {
            var vendor = _vendorService.GetById(id);
            var model = new ProductViewModel
            {
                Name = $"{vendor.Name}"
            };
            return PartialView("~/Areas/Admin/Views/vendor/_DeleteVendorPartial.cshtml", model);
        }

        [HttpPost]
        public ActionResult DeleteVendor(VendorViewModel model)
        {
            var vendor = _vendorService.GetById(model.Id);
            _vendorService.Delete(vendor);
            return RedirectToAction("Index");
        }

        private IList<VendorViewModel> GetVendors(IList<Vendor> vendors)
        {
            return vendors.Select(x => new VendorViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                PhoneNumber= x.PhoneNumber,
                CreateDate = x.CreatedDate,
                UpdateDate = x.UpdatedDate,
                Products = ConvertDomainToModel.GetProduct_SummaryInfo(_productService.GetProductsByVendorId(x.Id)),
                IsActive = x.IsActive
            }).ToList();
        }

    }
}