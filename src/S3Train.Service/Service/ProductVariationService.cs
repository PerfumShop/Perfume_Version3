using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Service
{
    public class ProductVariationService : GenenicServiceBase<ProductVariation>, IProductVariationService
    {
        public ProductVariationService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public ProductVariation GetOneProductVariations(Guid ProductId)
        {
            return this.EntityDbSet.FirstOrDefault(x => x.Product_Id == ProductId);
        }

        public ProductVariation GetProductVariationByIdAndVolume(Guid id, string volume)
        {
            return this.EntityDbSet.FirstOrDefault(x => x.Product_Id == id && x.Volume == volume);
        }

        public IList<ProductVariation> GetProductVariations(Guid ProductId)
        {
            return this.EntityDbSet.Where(x => x.Product_Id == ProductId).ToList();
        }
    }
}
