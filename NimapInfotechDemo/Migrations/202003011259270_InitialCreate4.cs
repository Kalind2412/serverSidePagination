namespace NimapInfotechDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Category", "CategoryName", c => c.String(nullable: false));
            AlterColumn("dbo.Product", "ProductName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "ProductName", c => c.String());
            AlterColumn("dbo.Category", "CategoryName", c => c.String());
        }
    }
}
