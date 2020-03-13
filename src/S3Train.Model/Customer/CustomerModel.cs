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
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        [Display(Name = "Town/City")]
        public string Town { get; set; }
        public string Phone { get; set; }
    }
}
