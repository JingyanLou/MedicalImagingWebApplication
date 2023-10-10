namespace FIT5032_MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class report : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Bookings", "ReportId", c => c.Int(nullable: true));
            CreateIndex("dbo.Bookings", "ReportId");
            AddForeignKey("dbo.Bookings", "ReportId", "dbo.Reports", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "ReportId", "dbo.Reports");
            DropIndex("dbo.Bookings", new[] { "ReportId" });
            DropColumn("dbo.Bookings", "ReportId");
            DropTable("dbo.Reports");
        }
    }
}
