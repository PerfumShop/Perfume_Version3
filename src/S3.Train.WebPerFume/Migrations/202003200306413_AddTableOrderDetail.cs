namespace S3.Train.WebPerFume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableOrderDetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Order", "ShoppingCart_Id", "dbo.ShoppingCart");
            DropIndex("dbo.Order", new[] { "ShoppingCart_Id" });
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Oder_Id = c.Guid(nullable: false),
                        ProductVariation_ID = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.Oder_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProductVariation", t => t.ProductVariation_ID, cascadeDelete: true)
                .Index(t => t.Oder_Id)
                .Index(t => t.ProductVariation_ID);
            
            AddColumn("dbo.Order", "Email", c => c.String());
            AddColumn("dbo.Order", "Note", c => c.String());
            DropColumn("dbo.Order", "ShoppingCart_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "ShoppingCart_Id", c => c.Guid(nullable: false));
            DropForeignKey("dbo.OrderDetail", "ProductVariation_ID", "dbo.ProductVariation");
            DropForeignKey("dbo.OrderDetail", "Oder_Id", "dbo.Order");
            DropIndex("dbo.OrderDetail", new[] { "ProductVariation_ID" });
            DropIndex("dbo.OrderDetail", new[] { "Oder_Id" });
            DropColumn("dbo.Order", "Note");
            DropColumn("dbo.Order", "Email");
            DropTable("dbo.OrderDetail");
            CreateIndex("dbo.Order", "ShoppingCart_Id");
            AddForeignKey("dbo.Order", "ShoppingCart_Id", "dbo.ShoppingCart", "Id", cascadeDelete: true);
        }
    }
}
