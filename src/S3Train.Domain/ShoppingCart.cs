using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Domain
{
    public class ShoppingCart:EntityBase
    {
        public DateTime OrderDate { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<ShoppingCartDetail> ShoppingCartDetails { get; set; }
    }

 
}
