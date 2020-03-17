using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Contract
{
    public interface IShoppingCartDetailService : IGenenicServiceBase<ShoppingCartDetail>
    {
        ShoppingCartDetail GetByProductIdAndCartShoppingCartId(Guid productId, Guid shoppingCarId);
        decimal? GetTotalPriceItem(Guid id);
        ICollection<ShoppingCartDetail> GetAllByCartId(Guid id);

    }
}
