namespace S3.Train.WebPerFume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeteleColumnPrice : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ShoppingCartDetail", "TotalPrice");
            DropColumn("dbo.ShoppingCart", "TotalPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingCart", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ShoppingCartDetail", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
