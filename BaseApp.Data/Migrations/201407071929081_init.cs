namespace BaseApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agolpaitos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Alias = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        AgolpaitoId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.AgolpaitoId)
                .ForeignKey("dbo.Agolpaitos", t => t.AgolpaitoId)
                .Index(t => t.AgolpaitoId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BillId = c.Int(nullable: false),
                        Bill_AgolpaitoId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bills", t => t.Bill_AgolpaitoId)
                .Index(t => t.Bill_AgolpaitoId);
            
            CreateTable(
                "dbo.Drinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SaleUnits = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PartyDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        PartyDayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PartyDays", t => t.PartyDayId, cascadeDelete: true)
                .Index(t => t.PartyDayId);
            
            CreateTable(
                "dbo.Party",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AgolpaitosDrinks",
                c => new
                    {
                        AgolpaitoId = c.Int(nullable: false),
                        DrinkId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AgolpaitoId, t.DrinkId })
                .ForeignKey("dbo.Agolpaitos", t => t.AgolpaitoId, cascadeDelete: true)
                .ForeignKey("dbo.Drinks", t => t.DrinkId, cascadeDelete: true)
                .Index(t => t.AgolpaitoId)
                .Index(t => t.DrinkId);
            
            CreateTable(
                "dbo.Attendance",
                c => new
                    {
                        AgolpaitoId = c.Int(nullable: false),
                        PartyDayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AgolpaitoId, t.PartyDayId })
                .ForeignKey("dbo.Agolpaitos", t => t.AgolpaitoId, cascadeDelete: true)
                .ForeignKey("dbo.PartyDays", t => t.PartyDayId, cascadeDelete: true)
                .Index(t => t.AgolpaitoId)
                .Index(t => t.PartyDayId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Attendance", "PartyDayId", "dbo.PartyDays");
            DropForeignKey("dbo.Attendance", "AgolpaitoId", "dbo.Agolpaitos");
            DropForeignKey("dbo.Event", "PartyDayId", "dbo.PartyDays");
            DropForeignKey("dbo.AgolpaitosDrinks", "DrinkId", "dbo.Drinks");
            DropForeignKey("dbo.AgolpaitosDrinks", "AgolpaitoId", "dbo.Agolpaitos");
            DropForeignKey("dbo.Bills", "AgolpaitoId", "dbo.Agolpaitos");
            DropForeignKey("dbo.Payments", "Bill_AgolpaitoId", "dbo.Bills");
            DropIndex("dbo.Attendance", new[] { "PartyDayId" });
            DropIndex("dbo.Attendance", new[] { "AgolpaitoId" });
            DropIndex("dbo.AgolpaitosDrinks", new[] { "DrinkId" });
            DropIndex("dbo.AgolpaitosDrinks", new[] { "AgolpaitoId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Event", new[] { "PartyDayId" });
            DropIndex("dbo.Payments", new[] { "Bill_AgolpaitoId" });
            DropIndex("dbo.Bills", new[] { "AgolpaitoId" });
            DropTable("dbo.Attendance");
            DropTable("dbo.AgolpaitosDrinks");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Party");
            DropTable("dbo.Event");
            DropTable("dbo.PartyDays");
            DropTable("dbo.Drinks");
            DropTable("dbo.Payments");
            DropTable("dbo.Bills");
            DropTable("dbo.Agolpaitos");
        }
    }
}
