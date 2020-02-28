﻿using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Service
{
    public class ProductImageService : GenenicServiceBase<ProductImage>, IProductImageService
    {
        public ProductImageService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public ProductImage GetProductImage(Guid proVa)
        {
            return this.EntityDbSet.FirstOrDefault(x => x.ProductVariation_Id == proVa);
        }
    }
}
