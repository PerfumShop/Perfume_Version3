using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Model.Customer
{
    public class CustomerModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }
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
