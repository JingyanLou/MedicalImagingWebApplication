namespace FIT5032_MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appoinmentstaffremove : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "StaffUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Appointments", new[] { "StaffUserId" });
            RenameColumn(table: "dbo.Appointments", name: "PatientUserId", newName: "DoctorUserId");
            RenameIndex(table: "dbo.Appointments", name: "IX_PatientUserId", newName: "IX_DoctorUserId");
            DropColumn("dbo.Appointments", "StaffUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "StaffUserId", c => c.String(nullable: false, maxLength: 128));
            RenameIndex(table: "dbo.Appointments", name: "IX_DoctorUserId", newName: "IX_PatientUserId");
            RenameColumn(table: "dbo.Appointments", name: "DoctorUserId", newName: "PatientUserId");
            CreateIndex("dbo.Appointments", "StaffUserId");
            AddForeignKey("dbo.Appointments", "StaffUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
