using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace S3Train.Model.Product
{
    public class ProductListModel
    {
        public IPagedList<ProductModel> productModels { get; set; }
    }
}
