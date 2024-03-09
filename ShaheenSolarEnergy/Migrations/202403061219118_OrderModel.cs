namespace ShaheenSolarEnergy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNumber = c.Int(nullable: false),
                        OrderStatus = c.String(),
                        OrderDate = c.DateTime(nullable: false),
                        BillTo = c.String(),
                        CustomerAddress = c.String(),
                        CustomerName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
        }
    }
}
