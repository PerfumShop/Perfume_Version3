using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Contract
{
    public interface IProductAdvertisement : IGenenicServiceBase<ProductAdvertisement>
    {

        ProductAdvertisement GetProductAdvertisementByType(ProductAdvertisementType type);

        IList<ProductAdvertisement> GetAllBannerByType(ProductAdvertisementType productAdvertisementType);
    }
}
