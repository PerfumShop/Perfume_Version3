using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Service
{
    public class BannerService : GenenicServiceBase<Banner>, IBannerService
    {
        public BannerService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public List<Banner> GetAllBannerSameType(BannerType bannerType)
        {
            return this.EntityDbSet.Where(b => b.AdType == bannerType && b.IsActive).ToList();
        }

        public Banner GetBannerByType(BannerType type)
        {
            return this.EntityDbSet.FirstOrDefault(b => b.AdType == type && b.IsActive);
        }
    }
}
