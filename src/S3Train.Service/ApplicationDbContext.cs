using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace S3Train.Domain
{
    public class ApplicationDbContext : 
        IdentityDbContext<ApplicationUser,ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet <ShoppingCart> ShoppingCarts{ get; set; }
        public DbSet<ShoppingCartDetail> ShoppingCartDetails { get; set; }
        public DbSet <ProductVariation> ProductVariations { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProductAdvertisement> ProductAdvertisements { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vendor>().ToTable("Vendor");
            modelBuilder.Entity<Vendor>().HasMany(p => p.Products).WithRequired(prod => prod.Vendor);   
            modelBuilder.Entity<Vendor>().Property(p => p.Name).HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Vendor>().Property(p => p.Address).HasMaxLength(120).IsRequired();
            modelBuilder.Entity<Vendor>().Property(p => p.Email).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Vendor>().Property(p => p.PhoneNumber).HasMaxLength(12).IsOptional();

            modelBuilder.Entity<Brand>().ToTable("Brand");
            modelBuilder.Entity<Brand>().HasMany(p => p.Products).WithRequired(prod => prod.Brand);
            modelBuilder.Entity<Brand>().Property(p => p.Name).HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Brand>().Property(p => p.Summary).HasMaxLength(400).IsRequired();
            modelBuilder.Entity<Brand>().Property(p => p.Logo).HasMaxLength(160).IsRequired();

            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Category>().HasOptional(p => p.ParentCategory).WithMany(c => c.ChildCategories).HasForeignKey(b => b.ParentId);
            modelBuilder.Entity<Category>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Category>().Property(x => x.Name).HasMaxLength(60).IsRequired();
            modelBuilder.Entity<Category>().Property(p => p.Summary).HasMaxLength(400).IsRequired();


            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Product>().HasMany(p => p.ProductVariations).WithRequired(s => s.Product).WillCascadeOnDelete();            modelBuilder.Entity<Product>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Product>().Property(x => x.Name).HasMaxLength(60).IsRequired();
            modelBuilder.Entity<Product>().Property(x => x.Description).HasMaxLength(int.MaxValue).IsRequired();
            modelBuilder.Entity<Product>().Property(x => x.ImagePath).IsRequired();
            modelBuilder.Entity<Product>().HasMany<Category>(f => f.Categories).WithMany(p => p.Products)
                .Map(pc =>
                {
                    pc.MapLeftKey("ProductId");
                    pc.MapRightKey("CategoryId");
                    pc.ToTable("ProductCategory");
                });

            modelBuilder.Entity<ProductVariation>().ToTable("ProductVariation");
            modelBuilder.Entity<ProductVariation>().Property(p => p.SKU ).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<ProductVariation>().Property(x => x.StockQuantity).IsRequired();
            modelBuilder.Entity<ProductVariation>().Property(p => p.Volume).HasMaxLength(10).IsRequired();
            modelBuilder.Entity<ProductVariation>().Property(x => x.Price).IsRequired();
            modelBuilder.Entity<ProductVariation>().Property(p => p.DiscountPrice).IsRequired();
            modelBuilder.Entity<ProductVariation>().HasMany(p => p.ProductImage).WithRequired(s => s.ProductVariation);
            modelBuilder.Entity<ProductVariation>().HasMany(p => p.ShoppingCartDetails).WithRequired(s => s.ProductVariation);
            modelBuilder.Entity<ProductVariation>().HasMany(p => p.OrderDetails).WithRequired(s => s.ProductVariation);

            modelBuilder.Entity<ProductImage>().ToTable("ProductImage");
            modelBuilder.Entity<ProductImage>().Property(x => x.ImagePath).HasMaxLength(100).IsRequired();

            modelBuilder.Entity<Banner>().ToTable("Banner");
            modelBuilder.Entity<Banner>().Property(x => x.Image).HasMaxLength(30);
            modelBuilder.Entity<Banner>().Property(p => p.Link).HasMaxLength(100);

            modelBuilder.Entity<ShoppingCart>().ToTable("ShoppingCart");
            modelBuilder.Entity<ShoppingCart>().Property(x => x.OrderDate).IsRequired();
            modelBuilder.Entity<ShoppingCart>().Property(x => x.UserId);
            modelBuilder.Entity<ShoppingCart>().HasMany(x => x.ShoppingCartDetails).WithRequired(s => s.ShoppingCart);

            modelBuilder.Entity<ShoppingCartDetail>().ToTable("ShoppingCartDetail");
            modelBuilder.Entity<ShoppingCartDetail>().Property(x => x.Quantity).IsRequired();

            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Order>().Property(x => x.DeliveryAddress).HasMaxLength(60).IsRequired();
            modelBuilder.Entity<Order>().Property(x => x.DeliveryName).HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Order>().Property(x => x.DeliveryPhone).HasMaxLength(12).IsRequired();
            modelBuilder.Entity<Order>().Property(x => x.OrderDate).IsRequired();
            modelBuilder.Entity<Order>().Property(x => x.Status).IsRequired();
            modelBuilder.Entity<Order>().Property(x => x.ToatalPrice).IsRequired();
            modelBuilder.Entity<Order>().Property(x => x.SubPrice).IsRequired();
            modelBuilder.Entity<Order>().Property(x => x.DeliveryFee).IsRequired();
            modelBuilder.Entity<Order>().HasMany(p => p.OrderDetails).WithRequired(s => s.Order);

            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetail");
            modelBuilder.Entity<OrderDetail>().Property(x => x.Quantity).IsRequired();
            modelBuilder.Entity<OrderDetail>().Property(x => x.TotalPrice).IsRequired();

            modelBuilder.Entity<ProductAdvertisement>().ToTable("ProductAdvertisement");
            modelBuilder.Entity<ProductAdvertisement>().Property(x => x.ImagePath).HasMaxLength(200).IsRequired();
            modelBuilder.Entity<ProductAdvertisement>().Property(x => x.EventUrl).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<ProductAdvertisement>().Property(x => x.EventUrlCaption).HasMaxLength(10).IsOptional();
            modelBuilder.Entity<ProductAdvertisement>().Property(x => x.Title).HasMaxLength(100).IsOptional();
            modelBuilder.Entity<ProductAdvertisement>().Property(x => x.Description).HasMaxLength(500).IsOptional();

        }
    }
}