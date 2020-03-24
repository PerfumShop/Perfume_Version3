using S3Train.Domain;
using System;
using System.Collections.Generic;

namespace S3Train.Model.Product
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual Domain.Brand Brand { get; set; }
        public virtual ICollection<ProductVariation> ProductVariations { get; set; }
        public virtual ICollection<Domain.Category> Categories { get; set; }
    }
}
