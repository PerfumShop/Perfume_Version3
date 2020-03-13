using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S3.Train.WebPerFume.Models;

namespace S3.Train.WebPerFume.Services
{
    public interface ICheckoutService
    {
        CheckoutViewModel GetCheckoutModel(object cookie);
        bool SaveOrder(CheckoutViewModel model);
    }
}
