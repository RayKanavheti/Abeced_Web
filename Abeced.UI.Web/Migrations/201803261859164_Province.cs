namespace Abeced.UI.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Province : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Province", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Province");
        }
    }
}
