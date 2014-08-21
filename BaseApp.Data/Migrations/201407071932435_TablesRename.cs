namespace BaseApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablesRename : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Event", newName: "Events");
            RenameTable(name: "dbo.Party", newName: "Parties");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Parties", newName: "Party");
            RenameTable(name: "dbo.Events", newName: "Event");
        }
    }
}
