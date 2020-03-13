using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S3.Train.WebPerFume.Models
{
    public class CartViewModel
    {
        public virtual ICollection<ShoppingCartDetail> ShoppingCartDetails { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }

    public class ShoppingCartModel
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ShoppingCartDetail> ShoppingCartDetails { get; set; }
    }

    public class ShoppingCartDetailModel
    {
        public Guid Id { get; set; }
        public Guid ShoppingCart_Id { get; set; }
        public int Quantity { get; set; }
        public Guid ProductVariation_Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual ProductVariation ProductVariation { get; set; }
    }
}