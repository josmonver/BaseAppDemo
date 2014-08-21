namespace BaseApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modeling2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payments", "Bill_AgolpaitoId", "dbo.Bills");
            DropIndex("dbo.Payments", new[] { "Bill_AgolpaitoId" });
            DropColumn("dbo.Payments", "BillId");
            RenameColumn(table: "dbo.Payments", name: "Bill_AgolpaitoId", newName: "BillId");
            AlterColumn("dbo.Payments", "BillId", c => c.Int(nullable: false));
            CreateIndex("dbo.Payments", "BillId");
            AddForeignKey("dbo.Payments", "BillId", "dbo.Bills", "AgolpaitoId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "BillId", "dbo.Bills");
            DropIndex("dbo.Payments", new[] { "BillId" });
            AlterColumn("dbo.Payments", "BillId", c => c.Int());
            RenameColumn(table: "dbo.Payments", name: "BillId", newName: "Bill_AgolpaitoId");
            AddColumn("dbo.Payments", "BillId", c => c.Int(nullable: false));
            CreateIndex("dbo.Payments", "Bill_AgolpaitoId");
            AddForeignKey("dbo.Payments", "Bill_AgolpaitoId", "dbo.Bills", "AgolpaitoId");
        }
    }
}
