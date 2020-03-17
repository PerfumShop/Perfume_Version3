using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Domain
{
    public class ShoppingCartDetail:EntityBase
    {
        [ForeignKey("ShoppingCart")]
        public Guid ShoppingCart_Id { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        [ForeignKey("ProductVariation")]
        public Guid ProductVariation_Id { get; set; }
        
        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual ProductVariation ProductVariation { get; set; }
    }
}
