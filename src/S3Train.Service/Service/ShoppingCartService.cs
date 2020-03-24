using S3Train.Contract;
using S3Train.Domain;
using System.Linq;
using System.Data.Entity;
using System;
using System.Collections.Generic;

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
            var result = query.FirstOrDefault(x => x.UserId == userId);

            return result;
        }

        public IList<ShoppingCart> GetShoppingCartNullOrThan30Days()
        {
            var query = this.EntityDbSet.Include(p => p.ShoppingCartDetails);
            var result = query.Where(p => p.ShoppingCartDetails.Count() == 0 || DbFunctions.DiffDays(p.CreatedDate,DateTime.Now) < 10 );
            return result.ToList();
        }
    }
}
