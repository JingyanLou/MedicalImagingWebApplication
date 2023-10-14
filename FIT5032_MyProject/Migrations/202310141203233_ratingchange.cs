namespace FIT5032_MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratingchange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ratings", "rate", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ratings", "rate", c => c.String(nullable: false));
        }
    }
}
