namespace ShaheenSolarEnergy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quotationModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Quotations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        UnitPrice = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        TotalCost = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quotations", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Quotations", "OrderId", "dbo.Orders");
            DropIndex("dbo.Quotations", new[] { "OrderId" });
            DropIndex("dbo.Quotations", new[] { "ProductId" });
            DropTable("dbo.Quotations");
        }
    }
}
