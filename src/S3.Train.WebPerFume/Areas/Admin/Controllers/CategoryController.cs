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
    public class CategoryController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public CategoryController()
        {

        }

        public CategoryController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: Admin/Category
        public ActionResult Index()
        {
            var model = GetCategorys(_categoryService.SelectAll());
            return View(model);
        }


        /// <summary>
        /// Detail Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Detail View</returns>
        public ActionResult DetailCategory(Guid id)
        {
            var category = _categoryService.GetById(id);
            var model = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId,
                CreatedDate = category.CreatedDate,
                IsActive = category.IsActive,
                Summary = category.Summary,
                ProductCategoriesModels = GetProducts(_productService.GetProductsByCategoryId(category.Id)),
                UpdatedDate = category.UpdatedDate,
            };
            return View(model);
        }

        #region Add or update category
        [HttpGet]
        public ActionResult AddOrEditCategory(Guid? id)
        {
            CategoryViewModel model = new CategoryViewModel();

            model.DropDownCategory = DropDownListDomain.DropDownList_Categoty(_categoryService.SelectAll().OrderBy(p => p.Name).ToList());
            model.DropDownProduct = DropDownListDomain.DropDownList_Product(_productService.SelectAll().OrderBy(p => p.Name).ToList());

            if (id.HasValue)
            {
                var category = _categoryService.GetById(id.Value);
                model.Id = category.Id;
                model.Name = category.Name;
                model.ParentId = category.ParentId;
                model.Summary = category.Summary;
                model.UpdatedDate = category.UpdatedDate;
                model.CreatedDate = category.CreatedDate;
                model.IsActive = category.IsActive;
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
        public ActionResult AddOrEditCategory(Guid? id, CategoryViewModel model)
        {
            try
            {
                bool isNew = !id.HasValue;
                var productList = model.SelectedProducts;

                // isNew = true update UpdatedDate of product
                // isNew = false get it by id
                var category = isNew ? new Category
                {
                    UpdatedDate = DateTime.Now
                } : _categoryService.GetById(id.Value);

                category.Name = model.Name;
                category.Summary = model.Summary;
                category.ParentId = model.ParentId;
                category.IsActive = true;

                var listProductId = model.SelectedProducts;

                if (isNew)
                {
                    category.CreatedDate = DateTime.Now;
                    category.Id = Guid.NewGuid();
                    _categoryService.Insert(category);

                    // insert product in category
                    InsertProductCategoryByListProId(listProductId,category.Id);
                }
                else
                {
                    _categoryService.Update(category);

                    // insert product in category
                    InsertProductCategoryByListProId(listProductId, category.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete Category
        [HttpGet]
        public PartialViewResult DeleteCategory(Guid id)
        {
            var category = _categoryService.GetById(id);
            var model = new ProductViewModel
            {
                Name = $"{category.Name}"
            };
            return PartialView("~/Areas/Admin/Views/category/_DeleteCategoryPartial.cshtml", model);
        }

        [HttpPost]
        public ActionResult DeleteCategory(CategoryViewModel model)
        {
            var category = _categoryService.GetById(model.Id);
            _categoryService.Delete(category);
            return RedirectToAction("Index");
        }
        #endregion

        /// <summary>
        /// Delete a Product on category
        /// </summary>
        /// <param name="product_Id">product</param>
        /// <param name="category_Id">category</param>
        /// <returns>View(Index)</returns>
        public ActionResult DeleteProductOnCategory(Guid product_Id, Guid category_Id)
        {
            _productService.DeleteProductOnCategory(category_Id, product_Id);
            return RedirectToAction("Index");
        }


        /// <summary>
        /// change status category
        /// </summary>
        /// <param name="category_Id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ActionResult ChangeStatusCategory(Guid category_Id, bool status)
        {
            var category = _categoryService.GetById(category_Id);
            _categoryService.ChangeStatus(category,status);
            return RedirectToAction("DetailCategory", new { id = category_Id});
        }

        private IList<CategoryViewModel> GetCategorys(IList<Category> categories)
        {
            return categories.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Summary = x.Summary,
                ParentId = x.ParentId,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                ProductCategoriesModels = GetProducts(_productService.GetProductsByCategoryId(x.Id)),
                IsActive = x.IsActive
            }).OrderBy(p => p.Name).ToList();
        }

        private IList<ProductViewModel> GetProducts(IList<Product> products)
        {
            return products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ImagePath = x.ImagePath
            }).OrderBy(p => p.Name).ToList();
        }

        private void InsertProductCategoryByListProId(List<Guid> listProId, Guid category_Id)
        {
            if (listProId != null)
            {
                foreach (var pro in listProId)
                {
                    _productService.InsertProductOnCategory(category_Id, pro);
                }
            }
        }

    }
}