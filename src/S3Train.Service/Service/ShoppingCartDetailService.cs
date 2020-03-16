using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace S3Train.Service
{
    public class ShoppingCartDetailService : GenenicServiceBase<ShoppingCartDetail>, IShoppingCartDetailService
    {
        public ShoppingCartDetailService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public ICollection<ShoppingCartDetail> GetAllByCartId(Guid id)
        {
            var query = this.EntityDbSet.Include(p => p.ProductVariation);
            var result = query.Where(x => x.ShoppingCart_Id == id);
            return result.ToList();
        }

        public ShoppingCartDetail GetByProductIdAndCartShoppingCartId(Guid productId, Guid shoppingCarId)
        {
            var query = this.EntityDbSet.Include(p => p.ShoppingCart);
            var result = query.FirstOrDefault(p => p.ShoppingCart_Id == shoppingCarId && p.ProductVariation_Id == productId);
            return result;
        }
    }
}
