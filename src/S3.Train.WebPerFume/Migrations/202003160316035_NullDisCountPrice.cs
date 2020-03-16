namespace S3.Train.WebPerFume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullDisCountPrice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductVariation", "DiscountPrice", c => c.Decimal(nullable: true));

        }

        public override void Down()
        {
            AlterColumn("dbo.ProductVariation", "DiscountPrice", c => c.Decimal(nullable: false));
        }
    }
}
