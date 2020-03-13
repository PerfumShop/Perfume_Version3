using S3Train.Contract;
using S3Train.Domain;
using System.Linq;
using System.Data.Entity;

namespace S3Train.Service
{
    public class ShoppingCartService : GenenicServiceBase<ShoppingCart>, IShoppingCartService
    {
        public ShoppingCartService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public ShoppingCart GetShoppingCartByUserId(string userId)
        {
            var query = this.EntityDbSet.Include(p => p.ShoppingCartDetails);
            return query.FirstOrDefault(x => x.UserId == userId);
        }
    }
}
