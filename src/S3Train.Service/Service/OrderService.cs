using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Service
{
    public class OrderService : GenenicServiceBase<Order>, IOrderService
    {
        public OrderService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Order GetOrderById(Guid id)
        {
            var query = this.EntityDbSet.Include(p => p.OrderDetails);
            var result = query.FirstOrDefault(p => p.Id == id);
            return result;
        }
    }
}
