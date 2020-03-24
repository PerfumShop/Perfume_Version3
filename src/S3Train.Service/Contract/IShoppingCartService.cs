using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Contract
{
    public interface IShoppingCartService : IGenenicServiceBase<ShoppingCart>
    {
        ShoppingCart GetShoppingCartByUserId(string userId);
        IList<ShoppingCart> GetShoppingCartNullOrThan30Days();
    }
}
