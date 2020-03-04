namespace S3.Train.WebPerFume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcolumnInUserAndRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetRoles", "Description", c => c.String(nullable: true));
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String(nullable: true));
            AddColumn("dbo.AspNetUsers", "Avatar", c => c.String(nullable:true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Avatar");
            DropColumn("dbo.AspNetUsers", "FullName");
            DropColumn("dbo.AspNetRoles", "Description");
        }
    }
}
