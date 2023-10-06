namespace FIT5032_MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class booking : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookableTimeSlotId = c.Int(nullable: false),
                        PatientUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookableTimeSlots", t => t.BookableTimeSlotId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.PatientUserId, cascadeDelete: true)
                .Index(t => t.BookableTimeSlotId)
                .Index(t => t.PatientUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "PatientUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookings", "BookableTimeSlotId", "dbo.BookableTimeSlots");
            DropIndex("dbo.Bookings", new[] { "PatientUserId" });
            DropIndex("dbo.Bookings", new[] { "BookableTimeSlotId" });
            DropTable("dbo.Bookings");
        }
    }
}
