using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
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


        /// <summary>
        /// get product variation with id and volume
        /// </summary>
        /// <param name="id">product variation id</param>
        /// <param name="volume"></param>
        /// <returns>product variation</returns>
        public ProductVariation GetProductVariationByIdAndVolume_version2(Guid id, string volume)
        {
            var query = this.EntityDbSet.Include(p => p.Product).Include(p => p.ProductImage);
            var model = query.FirstOrDefault(p => p.Product_Id == id && p.Volume == volume);
            return model;
        }

        public ProductVariation GetProductVariationByIdAndVolume(Guid id, string volume)
        {
            return this.EntityDbSet.FirstOrDefault(x => x.Product_Id == id && x.Volume == volume);
        }

        public IList<ProductVariation> GetProductVariations(Guid ProductId)
        {
            return this.EntityDbSet.Where(x => x.Product_Id == ProductId).Include(p=>p.ProductImage).ToList();
        }

        public string GetVolumeFisrtById(Guid id)
        {
            return this.EntityDbSet.FirstOrDefault(x => x.Product_Id == id).Volume;
        }
    }
}
