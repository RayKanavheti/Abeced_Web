namespace Abeced.UI.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Lname", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Lname");
        }
    }
}
