using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using S3Train.Contract;
using S3Train.Domain;
using S3Train.Model.Search;

namespace S3Train.Service
{
    public class ProductService : GenenicServiceBase<Product>, IProductService
    {
        public ProductService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IList<Product> GetProductsByBrandId(Guid brand_Id)
        {
            return this.EntityDbSet.Where(x => x.Brand_Id == brand_Id && x.IsActive == true).ToList();
        }
        public IList<Product> GetProductsByVendorId(Guid vendor_Id)
        {
            return this.EntityDbSet.Where(x => x.Vendor_Id == vendor_Id && x.IsActive == true).ToList();
        }

        public List<Product> GetProductsByCategoryId(Guid category_Id)
        {
            var query = from product in this.EntityDbSet
                        where product.Categories.Any(c => c.Id == category_Id)
                        select product;
            return query.ToList();
        }

        /// <summary>
        /// Insert Product in category
        /// </summary>
        /// <param name="category">category</param>
        /// <param name="product">Product</param>
        public void InsertProductOnCategory(Guid category_Id, Guid product_Id)
        {
            var category = DbContext.Categories.FirstOrDefault(c => c.Id == category_Id);
            var product = this.EntityDbSet.FirstOrDefault(p => p.Id == product_Id);
            var checkPro = category.Products.FirstOrDefault(p=>p.Id == product.Id);

            if(category != null && product != null)
            {
                if (checkPro != null)
                {
                    category.Products.Remove(checkPro);
                    category.Products.Add(product);
                }
                else
                {
                    category.Products.Add(product);
                }
            }
            
            
            this.DbContext.SaveChanges();
        }

        /// <summary>
        /// Delete Product in category
        /// </summary>
        /// <param name="category_Id"></param>
        /// <param name="product_Id"></param>
        public void DeleteProductOnCategory(Guid category_Id, Guid product_Id)
        {
            var category = DbContext.Categories.FirstOrDefault(c => c.Id == category_Id);
            var product = this.EntityDbSet.FirstOrDefault(p => p.Id == product_Id);
            var checkPro = category.Products.FirstOrDefault(p => p.Id == product.Id);

            if (category != null && product != null)
            {
                if (checkPro != null)
                {
                    category.Products.Remove(checkPro);
                }
            }
            this.DbContext.SaveChanges();
        }

        public IQueryable<Product> ManySearch(SearchViewModel model)
        {
            var query = this.EntityDbSet.Include(c => c.Categories).Include(p => p.ProductVariations).AsQueryable();

            if(!string.IsNullOrEmpty(model.SearchText))
            {
               query = query.Where(x => x.Name.Contains(model.SearchText) || x.Brand.Name.Contains(model.SearchText));
            }

            return query;
        }


        /// <summary>
        /// Get product by id in related 3 table
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>product</returns>
        public Product GetProductById(Guid id)
        {
            var query = this.EntityDbSet.Include(c => c.Categories).Include(p => p.ProductVariations);

            if(string.IsNullOrEmpty(id.ToString()))
                throw new NotImplementedException();

            var product = query.FirstOrDefault(p => p.Id == id);
            return product;
        }
    }
}