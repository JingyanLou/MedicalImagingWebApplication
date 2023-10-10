namespace FIT5032_MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newreportsystem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "ReportId", "dbo.Reports");
            DropIndex("dbo.Bookings", new[] { "ReportId" });
            AddColumn("dbo.Reports", "BookingId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reports", "BookingId");
            AddForeignKey("dbo.Reports", "BookingId", "dbo.Bookings", "Id", cascadeDelete: true);
            DropColumn("dbo.Bookings", "ReportId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "ReportId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Reports", "BookingId", "dbo.Bookings");
            DropIndex("dbo.Reports", new[] { "BookingId" });
            DropColumn("dbo.Reports", "BookingId");
            CreateIndex("dbo.Bookings", "ReportId");
            AddForeignKey("dbo.Bookings", "ReportId", "dbo.Reports", "Id", cascadeDelete: true);
        }
    }
}
