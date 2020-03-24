using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Service
{
    public class ProductAdvertisementService : GenenicServiceBase<ProductAdvertisement>, IProductAdvertisement
    {
        public ProductAdvertisementService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public ProductAdvertisement GetProductAdvertisementByType(ProductAdvertisementType type)
        {
            return this.EntityDbSet.FirstOrDefault(b => b.AdType == type && b.IsActive);
        }

        public IList<ProductAdvertisement> GetAllBannerByType(ProductAdvertisementType productAdvertisementType)
        {
            switch (productAdvertisementType)
            {
                case ProductAdvertisementType.SliderBanner:
                    return GetAllByType(productAdvertisementType);
                case ProductAdvertisementType.WomenSquareBanner:
                    return GetAllByType(productAdvertisementType);
                case ProductAdvertisementType.MenSquareBanner:
                    return GetAllByType(productAdvertisementType);
                case ProductAdvertisementType.UnisexSquareBanner:
                    return GetAllByType(productAdvertisementType);
                case ProductAdvertisementType.MidVertRectangleBanner:
                    break;
                case ProductAdvertisementType.LgVertRectangleBanner:
                    break;
                case ProductAdvertisementType.MidHorRectangleBanner:
                    break;
                case ProductAdvertisementType.LgHorRectangleBanner:
                    break;
                default:
                    break;
            }
            return null;
        }

        private IList<ProductAdvertisement> GetAllByType(ProductAdvertisementType type)
        {
            return this.EntityDbSet.Where(b => b.AdType == type && b.IsActive).ToList();
        }
    }
}
