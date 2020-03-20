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
        public decimal? SubTotal { get; set; }
        public decimal? Total { get; set; }
    }

    public class CustomerModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public string Name { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Last Name is required.")]
        public string Email { get; set; }
        public string Country { get; set; }
        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; }
        [Display(Name = "Town/City")]
        [Required(ErrorMessage = "Town/City is required.")]
        public string Town { get; set; }
        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }
    }

}