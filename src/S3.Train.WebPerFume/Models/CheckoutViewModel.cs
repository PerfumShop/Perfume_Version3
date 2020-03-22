using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S3.Train.WebPerFume.Models
{
    public class CheckoutViewModel
    {
        public CustomerModel customerModel { get; set; }
        public IList<ShoppingCartDetailModel> shoppingCartDetailModels { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public decimal DeliveryFee { get; set; }
    }

    public class CustomerModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Last Name is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [Phone]
        public string Phone { get; set; }

        public string Note { get; set; }
    }

}