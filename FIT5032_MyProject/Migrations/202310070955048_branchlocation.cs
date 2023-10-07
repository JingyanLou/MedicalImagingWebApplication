namespace FIT5032_MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class branchlocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "Latitude", c => c.Int(nullable: false));
            AddColumn("dbo.Branches", "Longitude", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Branches", "Longitude");
            DropColumn("dbo.Branches", "Latitude");
        }
    }
}
