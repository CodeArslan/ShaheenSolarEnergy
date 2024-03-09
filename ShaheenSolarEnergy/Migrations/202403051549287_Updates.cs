namespace ShaheenSolarEnergy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Suppliers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Suppliers", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Suppliers", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Suppliers", "Phone", c => c.String());
            AlterColumn("dbo.Suppliers", "Address", c => c.String());
            AlterColumn("dbo.Suppliers", "Name", c => c.String());
        }
    }
}
