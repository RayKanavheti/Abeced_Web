namespace Abeced.UI.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mynew : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Mynew");
           
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Mynew", c => c.String());
        }
    }
}
