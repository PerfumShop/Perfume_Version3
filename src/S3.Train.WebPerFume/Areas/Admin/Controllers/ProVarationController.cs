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
    public class ProVarationController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductVariationService _productVariationService;
        private readonly IProductImageService _productImageService;

        #region Ctor
        public ProVarationController()
        {

        }

        public ProVarationController(IProductService productService, IProductVariationService productVariationService, 
            IProductImageService productImageService)
        {
            _productService = productService;
            _productVariationService = productVariationService;
            _productImageService = productImageService;
        }
        #endregion

        #region Index and detail
        // GET: Admin/ProVaration
        public ActionResult Index()
        {
            try
            {
                var model = GetProducts(_productService.SelectAll().OrderBy(p => p.Name).ToList());
                return View(model);
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        public ActionResult Detail(Guid id)
        {
            try
            {
                var model = GetProducts(_productService.SelectAll().OrderBy(p => p.Name).ToList());
                return View(model);
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion

        #region Add or update Product Variation
        [HttpGet]
        public ActionResult AddOrEditProductVariation(Guid? id)
        {
            try
            {
                var model = new ProVarationViewModel();

                model.DropDownProduct = DropDownListDomain.DropDownList_Product(_productService.SelectAll());
                model.DropDownVolume = DropDownListDomain.DropDownList_Volume();

                if (id.HasValue)
                {
                    var productVariation = _productVariationService.GetById(id.Value);

                    model = ConvertDomainToModel.ConvertModelFromDomainToProVa(productVariation);

                    model.DropDownProduct = DropDownListDomain.DropDownList_Product(_productService.SelectAll());
                    model.DropDownVolume = DropDownListDomain.DropDownList_Volume();

                    model.Image = _productImageService.GetProductImage(model.Id) == null ? null :
                                            ConvertDomainToModel.GetProductImage(_productImageService.GetProductImage(model.Id));

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
        public ActionResult AddOrEditProductVariation(Guid? id, ProVarationViewModel model, IEnumerable<HttpPostedFileBase> imageList)
        {
            try
            {
                bool isNew = !id.HasValue;
                string localFile = Server.MapPath("~/Content/img/product-men");

                // isNew = true update UpdatedDate of product
                // isNew = false get it by id
                var productVariation = isNew ? new ProductVariation
                {
                    UpdatedDate = DateTime.Now
                } : _productVariationService.GetById(id.Value);

                productVariation.Product_Id = model.Product_Id;
                productVariation.SKU = model.SKU;
                productVariation.Volume = model.Volume;
                productVariation.Price = model.Price;
                productVariation.StockQuantity = model.StockQuantity;
                productVariation.IsActive = true;

                // if model.DiscountPrice != null then DiscountPrice =  model.DiscountPrice  else equal 0
                productVariation.DiscountPrice = (model.DiscountPrice.ToString() != null) ? model.DiscountPrice : 0;

                if (isNew)
                {
                    productVariation.CreatedDate = DateTime.Now;
                    productVariation.Id = Guid.NewGuid();
                    _productVariationService.Insert(productVariation);

                    // add many image for product variation
                    AddImageOnProductImageTable(imageList, productVariation.Id);
                }
                else
                {
                    _productVariationService.Update(productVariation);
                    
                    // Delete list image of product variation   
                    DeleteListImageOnProductImageTable(imageList,productVariation.Id);
                    // add many image for product variation
                    AddImageOnProductImageTable(imageList, productVariation.Id);
                }
                return RedirectToAction("DetailProduct", "Product", new { id = model.Product_Id });
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion

        #region Delete Product Variation and change status
        public ActionResult DeleteProductVariation(Guid id)
        {
            try
            {
                var product = _productVariationService.GetById(id);
                _productVariationService.Delete(product);
                return RedirectToAction("DetailProduct", "Product", new { id = product.Product_Id });
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }

        /// <summary>
        /// Change status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ActionResult ChangeStatus(Guid id, bool status)
        {
            try
            {
                var productVariation = _productVariationService.GetById(id);
                _productVariationService.ChangeStatus(productVariation, status);
                return RedirectToAction("DetailProduct", "Product", new { id = productVariation.Product_Id });
            }
            catch { return RedirectToAction("Erorr500", "HomdeAdmin"); }
        }
        #endregion

        #region Convert domain to model
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
                Product_Id = x.Product_Id,
                Price = x.Price,
                Volume = x.Volume,
                ProductImage = x.ProductImage,
                DiscountPrice =x.DiscountPrice,
                CreateDate = x.CreatedDate,
                UpdateDate = x.UpdatedDate,
                IsActive = x.IsActive
            }).ToList();
        }

        /// <summary>
        /// Get product have product variation inside
        /// </summary>
        /// <param name="products">Product List</param>
        /// <returns>ProductModel List</returns>
        public IList<ProductModel> GetProducts(IList<Product> products)
        {
            return products.Select(x => new ProductModel
            {
                Id = x.Id,
                ProductName = x.Name,
                ProductImage = x.ImagePath,
                proVarationViewModels = GetProductVariations(
                    _productVariationService.GetProductVariations(x.Id)).ToList(),
            }).ToList();
        }
        #endregion

        #region Add or delete product image 
        /// <summary>
        /// Add many image in product image table
        /// </summary>
        /// <param name="httpPostedFileBases">list image post</param>
        /// <param name="ProVaId">Product variation id</param>
        public void AddImageOnProductImageTable(IEnumerable<HttpPostedFileBase> httpPostedFileBases, Guid ProVaId)
        {
            string local = Server.MapPath("~/Content/img/product-men");
            foreach (var file in httpPostedFileBases)
            {
                var model = new ProductImage
                {
                    Id = Guid.NewGuid(),
                    ImagePath = _productImageService.UpFile(file,local),
                    ProductVariation_Id = ProVaId,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };
                _productImageService.Insert(model);
            }
        }

        public void DeleteListImageOnProductImageTable(IEnumerable<HttpPostedFileBase> httpPostedFileBases, Guid ProVaId)
        {
            string local = Server.MapPath("~/Content/img/product-men");
            foreach(var proImage in _productImageService.GetProductImageList(ProVaId))
            {
                _productImageService.Delete(proImage);
            }
        }
        #endregion
    }
}