namespace Abeced.UI.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EducationLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "EducationLevel", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "EducationLevel");
        }
    }
}
