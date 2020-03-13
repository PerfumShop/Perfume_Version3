using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Contract
{
    public interface IProductVariationService : IGenenicServiceBase<ProductVariation>
    {
        ProductVariation GetProductVariationByIdAndVolume(Guid id, string volume);
        IList<ProductVariation> GetProductVariations(Guid ProductId);
        ProductVariation GetOneProductVariations(Guid ProductId);
        ProductVariation GetProductVariationByIdAndVolume_version2(Guid id, string volume);
    }
}
