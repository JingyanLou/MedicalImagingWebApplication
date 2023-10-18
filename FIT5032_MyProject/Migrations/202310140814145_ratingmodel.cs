namespace FIT5032_MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratingmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        star = c.Int(nullable: false),
                        PatientUserId = c.String(nullable: false, maxLength: 128),
                        BranchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.PatientUserId, cascadeDelete: true)
                .Index(t => t.PatientUserId)
                .Index(t => t.BranchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "PatientUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ratings", "BranchId", "dbo.Branches");
            DropIndex("dbo.Ratings", new[] { "BranchId" });
            DropIndex("dbo.Ratings", new[] { "PatientUserId" });
            DropTable("dbo.Ratings");
        }
    }
}
