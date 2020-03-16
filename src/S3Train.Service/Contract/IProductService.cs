using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using S3Train.Domain;
using S3Train.Model.Search;

namespace S3Train.Contract
{
    public interface IProductService : IGenenicServiceBase<Product>
    {
        IList<Product> GetProductsByBrandId(Guid brand_Id);
        IList<Product> GetProductsByVendorId(Guid vendor_Id);
        List<Product> GetProductsByCategoryId(Guid category_Id);
        IQueryable<Product> ManySearch(SearchViewModel model);
        void InsertProductOnCategory(Guid category_Id, Guid product_Id);
        void DeleteProductOnCategory(Guid category_Id, Guid product_Id);
        List<Product> GetAllProduct(Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy);
        Product GetProductById(Guid id);
    }
}
