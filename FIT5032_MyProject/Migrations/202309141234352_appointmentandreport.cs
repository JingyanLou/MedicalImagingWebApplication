namespace FIT5032_MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentandreport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        BranchId = c.Int(nullable: false),
                        PatientUserId = c.String(nullable: false, maxLength: 128),
                        StaffUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.PatientUserId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.StaffUserId, cascadeDelete: true)
                .Index(t => t.BranchId)
                .Index(t => t.PatientUserId)
                .Index(t => t.StaffUserId);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ReportTime = c.DateTime(nullable: false),
                        ReportContent = c.String(),
                        AppointmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Appointments", t => t.AppointmentId, cascadeDelete: true)
                .Index(t => t.AppointmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "AppointmentId", "dbo.Appointments");
            DropForeignKey("dbo.Appointments", "StaffUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Appointments", "PatientUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Appointments", "BranchId", "dbo.Branches");
            DropIndex("dbo.Reports", new[] { "AppointmentId" });
            DropIndex("dbo.Appointments", new[] { "StaffUserId" });
            DropIndex("dbo.Appointments", new[] { "PatientUserId" });
            DropIndex("dbo.Appointments", new[] { "BranchId" });
            DropTable("dbo.Reports");
            DropTable("dbo.Appointments");
        }
    }
}
