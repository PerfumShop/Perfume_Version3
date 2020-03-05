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
        public ProductAdvertisement GetWomenSquareBanner()
        {
            return this.EntityDbSet.FirstOrDefault(b => b.AdType == ProductAdvertisementType.WomenSquareBanner && b.IsActive);
        }
        public ProductAdvertisement GetMenSquareBanner()
        {
            return this.EntityDbSet.FirstOrDefault(b => b.AdType == ProductAdvertisementType.MenSquareBanner && b.IsActive);
        }
        public ProductAdvertisement GetUnisexSquareBanner()
        {
            return this.EntityDbSet.FirstOrDefault(b => b.AdType == ProductAdvertisementType.UnisexSquareBanner && b.IsActive);
        }

        public ProductAdvertisement GetSliderBanner()
        {
            return this.EntityDbSet.FirstOrDefault(b => b.AdType == ProductAdvertisementType.SliderBanner && b.IsActive);
        }

        public IList<ProductAdvertisement> GetAllBannerByType(ProductAdvertisementType productAdvertisementType)
        {
            switch (productAdvertisementType)
            {
                case ProductAdvertisementType.SliderBanner:
                    return GetAllSliderBanner();
                case ProductAdvertisementType.WomenSquareBanner:
                    return GetAllWomenSquareBanner();
                case ProductAdvertisementType.MenSquareBanner:
                    return GetAllMenSquareBanner();
                case ProductAdvertisementType.UnisexSquareBanner:
                    return GetAllUnisexSquareBanner();
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
        private IList<ProductAdvertisement> GetAllWomenSquareBanner()
        {
            return this.EntityDbSet.Where(b => b.AdType == ProductAdvertisementType.WomenSquareBanner && b.IsActive).ToList();
        }
        private IList<ProductAdvertisement> GetAllMenSquareBanner()
        {
            return this.EntityDbSet.Where(b => b.AdType == ProductAdvertisementType.MenSquareBanner && b.IsActive).ToList();
        }
        private IList<ProductAdvertisement> GetAllUnisexSquareBanner()
        {
            return this.EntityDbSet.Where(b => b.AdType == ProductAdvertisementType.UnisexSquareBanner && b.IsActive).ToList();
        }
        private IList<ProductAdvertisement> GetAllSliderBanner()
        {
            return this.EntityDbSet.Where(b => b.AdType == ProductAdvertisementType.SliderBanner && b.IsActive).ToList();
        }
    }
}
