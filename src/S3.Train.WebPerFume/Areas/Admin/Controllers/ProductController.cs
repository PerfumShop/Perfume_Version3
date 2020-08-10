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
        private readonly ICategoryService _categoryService;

        #region Ctor
        public ProductController() { }

        public ProductController(IProductService productService, IBrandService brandService, ICategoryService categoryService,
            IVendorService vendorService, IProductVariationService productVariationService, IProductImageService productImageService)
        {
            _productService = productService;
            _brandService = brandService;
            _vendorService = vendorService;
            _productVariationService = productVariationService;
            _productImageService = productImageService;
            _categoryService = categoryService;
        }
        #endregion

        #region Index, paging and detail
        // GET: Admin/Product
        public ActionResult Index(int? pageIndex, int? pageSize)
        {
            try
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
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        
        public ActionResult DetailProduct(Guid id)
        {
            try
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
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion

        #region Create new or update product
        [HttpGet]
        public ActionResult AddOrEditProduct(Guid? id)
        {
            try
            {
                ProductViewModel model = new ProductViewModel();

                model.DropDownBrand = DropDownListDomain.DropDownList_Brand(_brandService.SelectAll());
                model.DropDownVendor = DropDownListDomain.DropDownList_Vendor(_vendorService.SelectAll());
                model.Volumes = DropDownListDomain.GetVolumeCheckBoxes();
                model.DropDowncategories = DropDownListDomain.DropDownList_Categoty(_categoryService.SelectAll());

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
                    model.IsActive = true;
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

                var listCategory = model.SelecteCategories;
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
                    InsertProductInManyCateory(listCategory, product.Id);
                    // insert product in category

                }
                else
                {
                    _productService.Update(product);
                    InsertProductInManyCateory(listCategory, product.Id);
                }
                return RedirectToAction("Index");
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion

        #region Delete or convert domain to model
        [HttpPost]
        public ActionResult DeleteProduct(ProductViewModel model)
        {
            try
            {
                var product = _productService.GetById(model.Id);
                _productService.Delete(product);
                return RedirectToAction("Index");
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
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
        #endregion

        #region Add product variation and change status
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
                IsActive = true,
                DiscountPrice = 0
            };

            _productVariationService.Insert(item);
        }
        public ActionResult ChangeStatusProduct(Guid product_Id, bool status)
        {
            try
            {
                var product = _productService.GetById(product_Id);
                _productService.ChangeStatus(product, status);
                return RedirectToAction("DetailProduct", new { id = product_Id });
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion

        // Add product in many categories
        public void InsertProductInManyCateory(IList<Guid> categories, Guid product_id)
        {
            if(categories != null)
            {
                foreach(var item in categories)
                    _productService.InsertProductOnCategory(item, product_id);
            }
        }
    }
}