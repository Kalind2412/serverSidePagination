namespace NimapInfotechDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "CategoryName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "CategoryName", c => c.String(nullable: false));
        }
    }
}
