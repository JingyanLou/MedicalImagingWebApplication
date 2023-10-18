namespace FIT5032_MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class branchrating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "AvgRating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Branches", "AvgRating");
        }
    }
}
