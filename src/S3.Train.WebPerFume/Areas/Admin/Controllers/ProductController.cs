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
using S3Train.Core.Constant;

namespace S3.Train.WebPerFume.Areas.Admin.Controllers
{
    [Authorize(Users = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly IVendorService _vendorService;
        private readonly IProductVariationService _productVariationService;
        private readonly IProductImageService _productImageService;


        public ProductController() { }

        public ProductController(IProductService productService, IBrandService brandService,
            IVendorService vendorService, IProductVariationService productVariationService, IProductImageService productImageService)
        {
            _productService = productService;
            _brandService = brandService;
            _vendorService = vendorService;
            _productVariationService = productVariationService;
            _productImageService = productImageService;
        }

        // GET: Admin/Product
        public ActionResult Index(int? pageIndex, int? pageSize)
        {
            pageIndex = (pageIndex ?? 1);
            pageSize = pageSize ?? GlobalConfigs.DEFAULT_PAGESIZE;

            var model = new ProductIndexViewModel()
            {
                PageIndex = pageIndex.Value,
                PageSize = pageSize.Value
            };

            var products = _productService.Gets(pageIndex, pageSize.Value, null, p => p.OrderBy(c => c.Name));

            model.Paged = products;
            model.Items = GetProducts(products.ToList());

            return View(model);
        }

        
        public ActionResult DetailProduct(Guid id)
        {
            var product = _productService.GetById(id);
            var model = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Brand = _brandService.GetById(product.Brand_Id),
                Vendor = _vendorService.GetById(product.Vendor_Id),
                ProVa = GetProductVariations(
                    _productVariationService.GetProductVariations(product.Id)).ToList(),
                Description = product.Description,
                ImagePath = product.ImagePath,
                CreatedDate = product.CreatedDate,
                UpdateDate = product.UpdatedDate,
                IsActive = product.IsActive
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult AddOrEditProduct(Guid? id)
        {
            ProductViewModel model = new ProductViewModel();

            model.DropDownBrand = DropDownListDomain.DropDownList_Brand(_brandService.SelectAll());
            model.DropDownVendor = DropDownListDomain.DropDownList_Vendor(_vendorService.SelectAll());
            model.Volumes = DropDownListDomain.GetVolumeCheckBoxes();

            if (id.HasValue)
            {
                var product = _productService.GetById(id.Value);
                model.Id = product.Id;
                model.Name = product.Name;
                model.Brand_Id = product.Brand_Id;
                model.Vendor_Id = product.Vendor_Id;
                model.Description = product.Description;
                model.ImagePath = product.ImagePath;
                model.CreateDate = product.CreatedDate;
                model.IsActive = product.IsActive;
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
        public ActionResult AddOrEditProduct(Guid? id, ProductViewModel model, HttpPostedFileBase image)
        {
            try
            {
                bool isNew = !id.HasValue;
                string localFile = Server.MapPath("~/Content/img/product-men");

                // isNew = true update UpdatedDate of product
                // isNew = false get it by id
                var product = isNew ? new Product
                {
                    UpdatedDate = DateTime.Now
                } : _productService.GetById(id.Value);

                product.Name = model.Name;
                product.Brand_Id = model.Brand_Id;
                product.Vendor_Id = model.Vendor_Id;
                product.Description = model.Description;
                product.ImagePath = _productService.UpFile(image, localFile);
                product.IsActive = true;

                if (isNew)
                {
                    product.CreatedDate = DateTime.Now;
                    product.Id = Guid.NewGuid();
                    _productService.Insert(product);

                    // Add ProductVariation if checked = true
                    foreach (var proVa in model.Volumes)
                    {
                        if (proVa.Checked)
                            AddProductVariation(product.Id, proVa.Volume);
                    }
                }
                else
                {
                    _productService.Update(product);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }



        [HttpGet]
        public PartialViewResult DeleteProduct(Guid id)
        {
            var product = _productService.GetById(id);
            var model = new ProductViewModel
            {
                Name = $"{product.Name}"
            };
            return PartialView("~/Areas/Admin/Views/Product/_DeleteProduct.cshtml", model);
        }

        [HttpPost]
        public ActionResult DeleteProduct(ProductViewModel model)
        {
            var product = _productService.GetById(model.Id);
            _productService.Delete(product);
            return RedirectToAction("Index");
        }


        /// <summary>
        /// Convert List Product to List ProductViewModel All Properties
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public List<ProductViewModel> GetProducts(IList<Product> products)
        {
            return products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Brand = _brandService.GetById(x.Brand_Id),
                Vendor = _vendorService.GetById(x.Vendor_Id),
                Description = x.Description,
                ProVa = GetProductVariations(
                    _productVariationService.GetProductVariations(x.Id)).ToList(),
                ImagePath = x.ImagePath,
                CreatedDate = x.CreatedDate,
                UpdateDate = x.UpdatedDate,
                IsActive = x.IsActive
            }).ToList();
        }

        /// <summary>
        /// get product variation have image with image id
        /// </summary>
        /// <param name="proVas">ProductVatiation List</param>
        /// <returns>ProVarationViewModel List</returns>
        public IList<ProVarationViewModel> GetProductVariations(IList<ProductVariation> proVas)
        {
            return proVas.Select(x => new ProVarationViewModel
            {
                Id = x.Id,
                SKU = x.SKU,
                StockQuantity = x.StockQuantity,
                Price = x.Price,
                Volume = x.Volume,
                ProductImage = x.ProductImage,
                DiscountPrice = x.DiscountPrice,
                CreateDate = x.CreatedDate,
                UpdateDate = x.UpdatedDate,
                IsActive = x.IsActive
            }).ToList();
        }


        /// <summary>
        /// Add Product Variation With product Id and volume
        /// </summary>
        /// <param name="product_Id">Product Id</param>
        /// <param name="volume">Volume of product variation</param>
        public void AddProductVariation(Guid product_Id, string volume)
        {
            var item = new ProductVariation
            {
                Id = Guid.NewGuid(),
                Product_Id = product_Id,
                Volume = volume,
                Price = 0,
                SKU = "Empty",
                StockQuantity = 0,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            _productVariationService.Insert(item);
        }
        public ActionResult ChangeStatusProduct(Guid product_Id, bool status)
        {
            var product = _productService.GetById(product_Id);
            _productService.ChangeStatus(product, status);
            return RedirectToAction("DetailProduct", new { id = product_Id });
        }

    }
}