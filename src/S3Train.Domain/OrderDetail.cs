using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Domain
{
    public class OrderDetail : EntityBase
    {
        [ForeignKey("Order")]
        public Guid Oder_Id { get; set; }

        [ForeignKey("ProductVariation")]
        public Guid ProductVariation_ID { get; set; }

        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual Order Order { get; set; }
        public virtual ProductVariation ProductVariation { get; set; }
    }
}
