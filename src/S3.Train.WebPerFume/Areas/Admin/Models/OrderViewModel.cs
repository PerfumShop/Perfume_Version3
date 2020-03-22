using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace S3.Train.WebPerFume.Areas.Admin.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public string DeliveryName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string DeliveryAddress { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string DeliveryPhone { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime OrderDate { get; set; }
        
        [Display(Name = "Create Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        public string Email { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }

        [Display(Name = "Toatal Price")]
        public decimal ToatalPrice { get; set; }

        [Display(Name = "Sub Price")]
        public decimal SubPrice { get; set; }

        [Display(Name = "Delivery Fee")]
        public decimal DeliveryFee { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}