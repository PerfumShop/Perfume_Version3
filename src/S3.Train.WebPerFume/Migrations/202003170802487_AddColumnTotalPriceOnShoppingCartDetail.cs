namespace S3.Train.WebPerFume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnTotalPriceOnShoppingCartDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCartDetail", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingCartDetail", "TotalPrice");
        }
    }
}
