namespace ShaheenSolarEnergy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelUpdateOrder : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "OrderStatus", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "CustomerName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "CustomerName", c => c.String());
            AlterColumn("dbo.Orders", "OrderStatus", c => c.String());
        }
    }
}
