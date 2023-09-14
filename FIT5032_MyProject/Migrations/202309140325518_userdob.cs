namespace FIT5032_MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userdob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DOB", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DOB");
        }
    }
}
