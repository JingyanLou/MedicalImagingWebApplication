namespace FIT5032_MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateappt : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Appointments", newName: "BookableTimeSlots");
            RenameColumn(table: "dbo.Reports", name: "AppointmentId", newName: "BookableTimeSlotId");
            RenameIndex(table: "dbo.Reports", name: "IX_AppointmentId", newName: "IX_BookableTimeSlotId");
            AddColumn("dbo.BookableTimeSlots", "IsAvailable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookableTimeSlots", "IsAvailable");
            RenameIndex(table: "dbo.Reports", name: "IX_BookableTimeSlotId", newName: "IX_AppointmentId");
            RenameColumn(table: "dbo.Reports", name: "BookableTimeSlotId", newName: "AppointmentId");
            RenameTable(name: "dbo.BookableTimeSlots", newName: "Appointments");
        }
    }
}
