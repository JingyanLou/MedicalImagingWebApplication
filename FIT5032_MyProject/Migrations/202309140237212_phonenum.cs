namespace FIT5032_MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phonenum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Phonenumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Phonenumber", c => c.String());
        }
    }
}
