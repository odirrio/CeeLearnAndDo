namespace CeeLearnAndDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removed_dates_from_applicationuser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "CreatedAt");
            DropColumn("dbo.AspNetUsers", "UpdatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "CreatedAt", c => c.DateTime(nullable: false));
        }
    }
}
