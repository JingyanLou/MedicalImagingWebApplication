namespace FIT5032_MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratewtf : DbMigration
    {
        public override void Up()
        {

            RenameColumn("dbo.Ratings", "star", "rate");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Ratings", "rate", "star");
        }
    }
    
}
