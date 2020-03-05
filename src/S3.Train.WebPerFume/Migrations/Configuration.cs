namespace S3.Train.WebPerFume.Migrations
{
    using S3Train.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var brands = new List<Brand>
            {
                new Brand
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,

                    Name = "Christian Dior",
                    Summary = "The 'Miss Dior Cherie' perfume and the 'Dior Homme' fragrance were released in 2005.",
                    Logo = "logodior",
                },
                 new Brand
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,

                    Name = "Chanel",
                    Summary = "Chanel is well-known for the perfume No. 5 de Chanel and Chanel Suit",
                    Logo = "logochanel",
                },
                new Brand
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,

                    Name = "Burberry",
                    Summary = "Burberry",
                    Logo = "logoburberry",
                },
                new Brand
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,

                    Name = "Gucci",
                    Summary = "is an Italian luxury brand",
                    Logo = "logogucci",
                },
                 new Brand
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,

                    Name = "Saint Laurent",
                    Summary = "",
                    Logo = "logoSaintLaurent",
                },
                new Brand
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,

                    Name = "Carven",
                    Summary = "",
                    Logo = "logocarven",
                },

                 new Brand
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,

                    Name = "Tom Ford",
                    Summary = "",
                    Logo = "logotomford",
                },
                 new Brand
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,

                    Name = "Van Cleef & Arpels",
                    Summary = "",
                    Logo = "logoVanCleef&Arpels",
                },
                  new Brand
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,

                    Name = "Le LaBo",
                    Summary = "",
                    Logo = "logolelabo",
                },
            };
            brands.ForEach(x => context.Brands.AddOrUpdate(c => c.Name, x));
            context.SaveChanges();
            var vendors = new List<Vendor>
            {
                 new Vendor
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Name = "Van Cleef & Arpels",
                    Address = "Paris",
                    PhoneNumber = "03030405",
                    Email="Vancleefarpels@gmail.com",
                },
                 new Vendor
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Name = "Carven",
                    Address = "Paris",
                    PhoneNumber = "03030405",
                    Email="carven@gmail.com",
                },
                 new Vendor
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Name = "YVesSaint Laurent",
                    Address = "Paris",
                    PhoneNumber = "03030405",
                    Email="YvesSaintLaurent@gmail.com",
                },
                 new Vendor
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Name = "Coco Chanel",
                    Address = "Paris",
                    PhoneNumber = "03030405",
                    Email="cocochanel@gmail.com",
                },
                 new Vendor
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Name = "Burberry",
                    Address = "United Kingdom",
                    PhoneNumber = "03030405",
                    Email="Burberry@gmail.com",
                },
                  new Vendor
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Name = "Gucci",
                    Address = "Italy",
                    PhoneNumber = "03030405",
                    Email="gucci@gmail.com",
                },
                  new Vendor
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Name = "Christian Dior",
                    Address = "Italy",
                    PhoneNumber = "03030405",
                    Email="dior@gmail.com",
                },
            };
            vendors.ForEach(x => context.Vendors.AddOrUpdate(c => c.Name, x));
            context.SaveChanges();
            var categories = new List<Category>
            {
            new Category
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Name = "Highlights",
                    Summary = "Typical products of the shop",

                },
            new Category
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Name = "Selling",
                    Summary = "Products sold the most",

                },
            new Category
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Name = "New Products",
                    Summary = "Launches new products",

                },
            new Category
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Name = "Promotion Item",
                    Summary = "Promotion products",

                },
            };
            categories.ForEach(x => context.Categories.AddOrUpdate(c => c.Name, x));
            context.SaveChanges();

            var banners = new List<Banner>
            {
            new Banner
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Image = "banner1",
                    Link ="banner1",
                    AdType = BannerType.MainBanner
                },
            new Banner
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Image = "banner2",
                    Link ="banner2",
                    AdType = BannerType.MenBanner
                },
            new Banner
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Image = "banner3",
                    Link ="banner3",
                    AdType = BannerType.WomenBanner
                },
            };
            banners.ForEach(x => context.Banners.AddOrUpdate(c => c.Image, x));
            context.SaveChanges();
            var productAds = new List<ProductAdvertisement>
                {
                    new ProductAdvertisement
                    {
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        IsActive = true,

                        EventUrl = "controller/action/1",
                        EventUrlCaption = string.Empty,
                        ImagePath = "http://placehold.it/900x350",
                        Title = "Adver One",
                        Description = "Lorem ipsum",
                        AdType = ProductAdvertisementType.SliderBanner
                    },
                    new ProductAdvertisement
                    {
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        IsActive = true,

                        EventUrl = "controller/action/11",
                        EventUrlCaption = string.Empty,
                        ImagePath = "http://placehold.it/900x350",
                        Title = "Adver One One",
                        Description = "Lorem ipsum",
                        AdType = ProductAdvertisementType.SliderBanner
                    },
                    new ProductAdvertisement
                    {
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        IsActive = true,

                        EventUrl = "controller/action/2",
                        EventUrlCaption = string.Empty,
                        ImagePath = "http://placehold.it/900x350",
                        Title = "Adver Two",
                        Description = "Lorem ipsum",
                        AdType = ProductAdvertisementType.WomenSquareBanner
                    },
                    new ProductAdvertisement
                    {
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        IsActive = true,

                        EventUrl = "controller/action/3",
                        EventUrlCaption = string.Empty,
                        ImagePath = "http://placehold.it/900x350",
                        Title = "Adver Three",
                        Description = "Lorem ipsum",
                        AdType = ProductAdvertisementType.MenSquareBanner
                    },
                    new ProductAdvertisement
                    {
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        IsActive = true,

                        EventUrl = "controller/action/4",
                        EventUrlCaption = string.Empty,
                        ImagePath = "http://placehold.it/900x350",
                        Title = "Adver Four",
                        Description = "Lorem ipsum",
                        AdType = ProductAdvertisementType.UnisexSquareBanner
                    },
                    new ProductAdvertisement
                    {
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        IsActive = true,

                        EventUrl = "controller/action/5",
                        EventUrlCaption = string.Empty,
                        ImagePath = "http://placehold.it/900x350",
                        Title = "Adver Five",
                        Description = "Lorem ipsum",
                        AdType = ProductAdvertisementType.MidVertRectangleBanner
                    },
                    new ProductAdvertisement
                    {
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        IsActive = true,

                        EventUrl = "controller/action/6",
                        EventUrlCaption = string.Empty,
                        ImagePath = "http://placehold.it/900x350",
                        Title = "Adver Six",
                        Description = "Lorem ipsum",
                        AdType = ProductAdvertisementType.MidHorRectangleBanner
                    },
                    new ProductAdvertisement
                    {
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        IsActive = true,

                        EventUrl = "controller/action/7",
                        EventUrlCaption = string.Empty,
                        ImagePath = "http://placehold.it/900x350",
                        Title = "Adver Seven",
                        Description = "Lorem ipsum",
                        AdType = ProductAdvertisementType.LgVertRectangleBanner
                    },
                    new ProductAdvertisement
                    {
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        IsActive = true,

                        EventUrl = "controller/action/8",
                        EventUrlCaption = string.Empty,
                        ImagePath = "http://placehold.it/900x350",
                        Title = "Adver Eight",
                        Description = "Lorem ipsum",
                        AdType = ProductAdvertisementType.LgHorRectangleBanner
                    },

                };
            productAds.ForEach(x => context.ProductAdvertisements.AddOrUpdate(p => p.Title, x));
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product
                {
                    Id= Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Name= "Carven Eau de Toilette",
                    Description="A wave of freshness and freedom for a fresher and more luminous expression of the Carven style.",
                    ImagePath = "product1",
                    Vendor_Id=vendors.Single(x=>x.Name.Equals("Carven",StringComparison.OrdinalIgnoreCase)).Id,
                    Brand_Id= brands.Single(x=>x.Name.Equals("Carven",StringComparison.OrdinalIgnoreCase)).Id,
                },
                new Product
                {
                    Id= Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Name = "Acqua Di Parma",
                    ImagePath = "product2",
                    Description = "A wave of freshness and freedom for a fresher and more luminous expression of the Carven style.",
                    Vendor_Id = vendors.Single(x => x.Name.Equals("Carven", StringComparison.OrdinalIgnoreCase)).Id,
                    Brand_Id = brands.Single(x => x.Name.Equals("Carven", StringComparison.OrdinalIgnoreCase)).Id,
                },
                new Product
                {
                    Id= Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Name= "Mandarino di Mmalfi",
                    ImagePath = "product3",
                    Description="A wave of freshness and freedom for a fresher and more luminous expression of the Carven style.",
                    Vendor_Id=vendors.Single(x=>x.Name.Equals("Gucci",StringComparison.OrdinalIgnoreCase)).Id,
                    Brand_Id= brands.Single(x=>x.Name.Equals("Tom Ford",StringComparison.OrdinalIgnoreCase)).Id,
                },
                new Product
                {
                    Id= Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Name= "Bis",
                    ImagePath = "product4",
                    Description="A wave of freshness and freedom for a fresher and more luminous expression of the Carven style.",
                    Vendor_Id=vendors.Single(x=>x.Name.Equals("Gucci",StringComparison.OrdinalIgnoreCase)).Id,
                    Brand_Id= brands.Single(x=>x.Name.Equals("Le LaBo",StringComparison.OrdinalIgnoreCase)).Id,
                },
            };
            products.ForEach(s => context.Products.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();
            var productvariations = new List<ProductVariation>
            {
                new ProductVariation
                {
                    Id= Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    SKU= "CCEdT100f",
                    Volume="100ml",
                    StockQuantity=2,
                    Price=90,

                    Product_Id=products.Single(x=>x.Name.Equals("Carven Eau de Toilette",StringComparison.OrdinalIgnoreCase)).Id,
                },
                 new ProductVariation
                {
                    Id= Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    SKU= "CCEdT50f",
                    Volume="50ml",
                    StockQuantity=2,
                    Price=60,

                    Product_Id=products.Single(x=>x.Name.Equals("Carven Eau de Toilette",StringComparison.OrdinalIgnoreCase)).Id,
                },
                new ProductVariation
                {
                    Id= Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    SKU= "AADP50m",
                    Volume="50ml",
                    StockQuantity=2,
                    Price=60,

                    Product_Id=products.Single(x=>x.Name.Equals("Acqua Di Parma",StringComparison.OrdinalIgnoreCase)).Id,
                },
                 new ProductVariation
                {
                    Id= Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    SKU= "TMdM100f",
                    Volume="100ml",
                    StockQuantity=5,
                    Price=60,

                    Product_Id=products.Single(x=>x.Name.Equals("Mandarino di Mmalfi",StringComparison.OrdinalIgnoreCase)).Id,
                },
            };
            productvariations.ForEach(s => context.ProductVariations.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();

            var productimages = new List<ProductImage>
            {
            new ProductImage
            {
                Id= Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                IsActive = true,
                ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("CCEdT100f",StringComparison.OrdinalIgnoreCase)).Id,
                ImagePath="girl_3.jpeg",
            },
            new ProductImage
            {
                Id= Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                IsActive = true,
                ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("CCEdT100f",StringComparison.OrdinalIgnoreCase)).Id,
                ImagePath="girl_3.jpeg",
            },
            new ProductImage
            {
                Id= Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                IsActive = true,
                ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("CCEdT50f",StringComparison.OrdinalIgnoreCase)).Id,
                ImagePath="girl-tomford.PNG",
            },
            new ProductImage
            {
                Id= Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                IsActive = true,
                ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("CCEdT50f",StringComparison.OrdinalIgnoreCase)).Id,
                ImagePath="girl-tomford.PNG",
            },
            new ProductImage
            {
                Id= Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                IsActive = true,
                ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("AADP50m",StringComparison.OrdinalIgnoreCase)).Id,
                ImagePath="girl-tomford.PNG",
            },
            new ProductImage
            {
                Id= Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                IsActive = true,
                ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("AADP50m",StringComparison.OrdinalIgnoreCase)).Id,
                ImagePath="girl-subtle1.jpeg",
            },
            new ProductImage
            {
                Id= Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                IsActive = true,
                ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("TMdM100f",StringComparison.OrdinalIgnoreCase)).Id,
                ImagePath="girl-subtle1.jpeg",
            },
            };
            productimages.ForEach(s => context.ProductImages.AddOrUpdate(x => x.Id, s));
            context.SaveChanges();







            ////var products = new List<Product>
            ////{
            ////    new Product
            ////    {
            ////        Id= Guid.NewGuid(),
            ////        CreatedDate = DateTime.Now,
            ////        IsActive = true,
            ////        Name= "Carven Eau de Toilette",
            ////        Description="A wave of freshness and freedom for a fresher and more luminous expression of the Carven style.",
            ////        Vendor_Id=vendors.Single(x=>x.Name.Equals("Carven",StringComparison.OrdinalIgnoreCase)).Id,
            ////        Brand_Id= brands.Single(x=>x.Name.Equals("Carven",StringComparison.OrdinalIgnoreCase)).Id,
            ////    },
            ////    new Product
            ////    {
            ////        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        Name= "Acqua Di Parma",
            //        Description="A wave of freshness and freedom for a fresher and more luminous expression of the Carven style.",
            //        Vendor_Id=vendors.Single(x=>x.Name.Equals("Carven",StringComparison.OrdinalIgnoreCase)).Id,
            //        Brand_Id= brands.Single(x=>x.Name.Equals("Carven",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //    new Product
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        Name= "Mandarino di Mmalfi",
            //        Description="A wave of freshness and freedom for a fresher and more luminous expression of the Carven style.",
            //        Vendor_Id=vendors.Single(x=>x.Name.Equals("Gucci",StringComparison.OrdinalIgnoreCase)).Id,
            //        Brand_Id= brands.Single(x=>x.Name.Equals("Tom Ford",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //    new Product
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        Name= "Bis",
            //        Description="A wave of freshness and freedom for a fresher and more luminous expression of the Carven style.",
            //        Vendor_Id=vendors.Single(x=>x.Name.Equals("Gucci",StringComparison.OrdinalIgnoreCase)).Id,
            //        Brand_Id= brands.Single(x=>x.Name.Equals("RODIN",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //    new Product
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        Name= "Cuir Cannage",
            //        Description="A wave of freshness and freedom for a fresher and more luminous expression of the Carven style.",
            //        Vendor_Id=vendors.Single(x=>x.Name.Equals("Christian Dior",StringComparison.OrdinalIgnoreCase)).Id,
            //        Brand_Id= brands.Single(x=>x.Name.Equals("Christian Dior",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //     new Product
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        Name= "SauVage",
            //        Description="A wave of freshness and freedom for a fresher and more luminous expression of the Carven style.",
            //        Vendor_Id=vendors.Single(x=>x.Name.Equals("Christian Dior",StringComparison.OrdinalIgnoreCase)).Id,
            //        Brand_Id= brands.Single(x=>x.Name.Equals("Christian Dior",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //     new Product
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        Name= "Van Cleef & Arpels",
            //        Description="A wave of freshness and freedom for a fresher and more luminous expression of the Carven style.",
            //        Vendor_Id=vendors.Single(x=>x.Name.Equals("Van Cleef & Arpels",StringComparison.OrdinalIgnoreCase)).Id,
            //        Brand_Id= brands.Single(x=>x.Name.Equals("Van Cleef & Arpels",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //     new Product
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        Name= "LELABOANOTHER13",
            //        Description="A wave of freshness and freedom for a fresher and more luminous expression of the Carven style.",
            //        Vendor_Id=vendors.Single(x=>x.Name.Equals("Christian Dior",StringComparison.OrdinalIgnoreCase)).Id,
            //        Brand_Id= brands.Single(x=>x.Name.Equals("LE LABO",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //    new Product
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        Name= "Gucci Bloom",
            //        Description="A wave of freshness and freedom for a fresher and more luminous expression of the Carven style.",
            //        Vendor_Id=vendors.Single(x=>x.Name.Equals("Gucci",StringComparison.OrdinalIgnoreCase)).Id,
            //        Brand_Id= brands.Single(x=>x.Name.Equals("Gucci",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //    new Product
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        Name= "Gucci Guilty Love Edition Pour Homme",
            //        Description="The celebration of liberty and contemporary love continues with the new Gucci Guilty Love Edition scents, Pour Homme and Pour Femme.",
            //        Vendor_Id=vendors.Single(x=>x.Name.Equals("Gucci",StringComparison.OrdinalIgnoreCase)).Id,
            //        Brand_Id= brands.Single(x=>x.Name.Equals("Gucci",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //};
            //products.ForEach(s => context.Products.AddOrUpdate(p => p.Name, s));
            //context.SaveChanges();
            //var productvariations = new List<ProductVariation>
            //{
            //    new ProductVariation
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        SKU= "CCEdT100f",
            //        Volume="100ml",
            //        StockQuantity=2,
            //        Price=90,

            //        Product_Id=products.Single(x=>x.Name.Equals("Carven Eau de Toilette",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //     new ProductVariation
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        SKU= "CCEdT50f",
            //        Volume="50ml",
            //        StockQuantity=2,
            //        Price=60,

            //        Product_Id=products.Single(x=>x.Name.Equals("Carven Eau de Toilette",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //    new ProductVariation
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        SKU= "AADP50m",
            //        Volume="50ml",
            //        StockQuantity=2,
            //        Price=60,

            //        Product_Id=products.Single(x=>x.Name.Equals("Acqua Di Parma",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //     new ProductVariation
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        SKU= "TMdM100f",
            //        Volume="100ml",
            //        StockQuantity=5,
            //        Price=60,

            //        Product_Id=products.Single(x=>x.Name.Equals("Mandarino di Mmalfi",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //      new ProductVariation
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        SKU= "Llba13100u",
            //        Volume="100ml",
            //        StockQuantity=2,
            //        Price=75,
            //        DiscountPrice=70,
            //        Product_Id=products.Single(x=>x.Name.Equals("LELABOANOTHER13",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //    new ProductVariation
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        SKU= "Llba1350u",
            //        Volume="50ml",
            //        StockQuantity=7,
            //        Price=40,
            //        Product_Id=products.Single(x=>x.Name.Equals("LELABOANOTHER13",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //    new ProductVariation
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        SKU= "Dsv50m",
            //        Volume="50ml",
            //        StockQuantity=9,
            //        Price=35,
            //        Product_Id=products.Single(x=>x.Name.Equals("SauVage",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //     new ProductVariation
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        SKU= "Dsv100m",
            //        Volume="100ml",
            //        StockQuantity=3,
            //        Price=60,
            //        Product_Id=products.Single(x=>x.Name.Equals("SauVage",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //      new ProductVariation
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        SKU= "Vvca100m",
            //        Volume="100ml",
            //        StockQuantity=10,
            //        Price=85,
            //        Product_Id=products.Single(x=>x.Name.Equals("Van Cleef & Arpels",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //         new ProductVariation
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        SKU= "Dsv20m",
            //        Volume="20ml",
            //        StockQuantity=5,
            //        Price=10,
            //        Product_Id=products.Single(x=>x.Name.Equals("SauVage",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //      new ProductVariation
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        SKU= "Vvca50m",
            //        Volume="50ml",
            //        StockQuantity=8,
            //        Price=50,
            //        Product_Id=products.Single(x=>x.Name.Equals("Van Cleef & Arpels",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //    new ProductVariation
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        SKU= "Ggb100f",
            //        Volume="100ml",
            //        StockQuantity=9,
            //        Price=85,
            //        Product_Id=products.Single(x=>x.Name.Equals("Gucci Bloom",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //     new ProductVariation
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        SKU= "Dcc100f",
            //        Volume="100ml",
            //        StockQuantity=9,
            //        Price=85,
            //        DiscountPrice=80,
            //        Product_Id=products.Single(x=>x.Name.Equals("Cuir Cannage",StringComparison.OrdinalIgnoreCase)).Id,
            //    },
            //       new ProductVariation
            //    {
            //        Id= Guid.NewGuid(),
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        SKU= "Dcc150f",
            //        Volume="150ml",
            //        StockQuantity=9,
            //        Price=120,
            //        Product_Id=products.Single(x=>x.Name.Equals("Cuir Cannage",StringComparison.OrdinalIgnoreCase)).Id,
            //    },

            //};
            //productvariations.ForEach(s => context.ProductVariations.AddOrUpdate(p => p.SKU, s));
            //context.SaveChanges();
            //var productimages = new List<ProductImage>
            //{
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("CCEdT100f",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl_3.jpeg",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("CCEdT100f",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl_3.jpeg",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("CCEdT50f",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-tomford.PNG",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("CCEdT50f",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-tomford.PNG",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("AADP50m",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-tomford.PNG",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("AADP50m",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-subtle1.jpeg",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("TMdM100f",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-subtle1.jpeg",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("Llba13100u",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-subtle1.jpeg",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("Llba13100u",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-magnolia.PNG",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("Llba1350u",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-magnolia.PNG",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("Dsv50m",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-magnolia.PNG",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("Dsv50m",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-magnolia.PNG",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("Dsv100m",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-magnolia.PNG",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("Dsv100m",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-magnolia.PNG",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("Dsv20m",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-magnolia.PNG",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("Vvca100m",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-lavie.PNG",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("Vvca100m",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-lavie.PNG",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("Vvca50m",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-lavie.PNG",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("Vvca50m",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-chloe.PNG",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("Ggb100f",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-chloe.PNG",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("Dcc100f",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-chloe.PNG",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("Dcc100f",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-carven.PNG",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("Dcc150f",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-carven.PNG",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("RB100f",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-acqua.PNG",
            //},
            //new ProductImage
            //{
            //    Id= Guid.NewGuid(),
            //    CreatedDate = DateTime.Now,
            //    IsActive = true,
            //    ProductVariation_Id= productvariations.Single(x=>x.SKU.Equals("RB100f",StringComparison.OrdinalIgnoreCase)).Id,
            //    ImagePath="girl-acqua.PNG",
            //},
            //};
            //productimages.ForEach(s => context.ProductImages.AddOrUpdate(x => x.Id));
            //context.SaveChanges();

        }
    }
}
