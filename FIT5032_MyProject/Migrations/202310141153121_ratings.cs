namespace FIT5032_MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        rate = c.String(nullable: false),
                        BranchId = c.Int(nullable: false),
                        PatientUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.PatientUserId, cascadeDelete: true)
                .Index(t => t.BranchId)
                .Index(t => t.PatientUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "PatientUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ratings", "BranchId", "dbo.Branches");
            DropIndex("dbo.Ratings", new[] { "PatientUserId" });
            DropIndex("dbo.Ratings", new[] { "BranchId" });
            DropTable("dbo.Ratings");
        }
    }
}
