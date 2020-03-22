namespace S3.Train.WebPerFume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoreColumnInTableOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "ToatalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Order", "SubPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Order", "DeliveryFee", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "DeliveryFee");
            DropColumn("dbo.Order", "SubPrice");
            DropColumn("dbo.Order", "ToatalPrice");
        }
    }
}
