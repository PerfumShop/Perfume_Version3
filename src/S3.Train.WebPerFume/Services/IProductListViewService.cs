using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S3Train.Domain;
using S3Train.Model.Product;

namespace S3.Train.WebPerFume.Services
{
    public interface IProductListViewService
    {
        ProductListModel GetProductListViewModel(int? currentPage, string searchFilter, string searchValue, string sortOrder);
        IQueryable<Product> GetProducts(int? currentPage, string searchFilter, string searchValue, string sortOrder);
    }
}
