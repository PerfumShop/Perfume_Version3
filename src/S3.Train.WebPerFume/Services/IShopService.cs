using S3.Train.WebPerFume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3.Train.WebPerFume.Services
{
    public interface IShopService
    {
        ShopViewModel GetShopViewModel(int? currentPage, string searchFilter, string searchValue, string sortOrder);
    }
}
