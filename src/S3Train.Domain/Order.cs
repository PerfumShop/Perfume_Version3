using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Domain
{
    public class Order:EntityBase
    {
        public string DeliveryName { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryPhone { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal ToatalPrice { get; set; }
        public decimal SubPrice { get; set; }
        public decimal DeliveryFee { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
