namespace Abeced.UI.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Occupation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Occupation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Occupation");
        }
    }
}
