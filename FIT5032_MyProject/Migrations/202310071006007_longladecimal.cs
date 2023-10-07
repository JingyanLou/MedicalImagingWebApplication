namespace FIT5032_MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class longladecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Branches", "Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Branches", "Longitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Branches", "Longitude", c => c.Int(nullable: false));
            AlterColumn("dbo.Branches", "Latitude", c => c.Int(nullable: false));
        }
    }
}
