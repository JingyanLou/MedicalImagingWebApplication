namespace FIT5032_MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removemodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reports", "BookableTimeSlotId", "dbo.BookableTimeSlots");
            DropIndex("dbo.Reports", new[] { "BookableTimeSlotId" });
            DropTable("dbo.Reports");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ReportTime = c.DateTime(nullable: false),
                        ReportContent = c.String(),
                        BookableTimeSlotId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateIndex("dbo.Reports", "BookableTimeSlotId");
            AddForeignKey("dbo.Reports", "BookableTimeSlotId", "dbo.BookableTimeSlots", "Id", cascadeDelete: true);
        }
    }
}
