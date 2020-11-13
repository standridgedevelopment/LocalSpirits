namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Beer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Aroma = c.String(),
                        Taste = c.String(),
                        ABV = c.Int(nullable: false),
                        Hops = c.String(),
                        Package = c.String(),
                        KegsAvailable = c.Boolean(nullable: false),
                        Availability = c.String(),
                        website = c.String(),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Brewery",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CityID = c.Int(nullable: false),
                        ZipCode = c.Int(nullable: false),
                        Hours = c.String(),
                        PhoneNumber = c.String(),
                        Website = c.String(),
                        Rating = c.Int(nullable: false),
                        LiveMusic = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.City", t => t.CityID, cascadeDelete: true)
                .ForeignKey("dbo.Zipcode", t => t.ZipCode, cascadeDelete: true)
                .Index(t => t.CityID)
                .Index(t => t.ZipCode);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StateID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.State", t => t.StateID)
                .Index(t => t.StateID);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Zipcode",
                c => new
                    {
                        ZipCode = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ZipCode);
            
            CreateTable(
                "dbo.Distillery",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CityID = c.Int(nullable: false),
                        ZipCode = c.Int(nullable: false),
                        Hours = c.String(),
                        PhoneNumber = c.String(),
                        Website = c.String(),
                        Rating = c.Int(nullable: false),
                        LiveMusic = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.City", t => t.CityID, cascadeDelete: true)
                .ForeignKey("dbo.Zipcode", t => t.ZipCode, cascadeDelete: true)
                .Index(t => t.CityID)
                .Index(t => t.ZipCode);
            
            CreateTable(
                "dbo.Liquor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ABV = c.Int(nullable: false),
                        Package = c.Int(nullable: false),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        Website = c.String(),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Profile",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        ZipCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Winery",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CityID = c.Int(nullable: false),
                        ZipCode = c.Int(nullable: false),
                        Hours = c.String(),
                        PhoneNumber = c.String(),
                        Website = c.String(),
                        Rating = c.Int(nullable: false),
                        LiveMusic = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.City", t => t.CityID, cascadeDelete: true)
                .ForeignKey("dbo.Zipcode", t => t.ZipCode, cascadeDelete: true)
                .Index(t => t.CityID)
                .Index(t => t.ZipCode);
            
            CreateTable(
                "dbo.Wine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Package = c.Int(nullable: false),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        Website = c.String(),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Winery", "ZipCode", "dbo.Zipcode");
            DropForeignKey("dbo.Winery", "CityID", "dbo.City");
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Distillery", "ZipCode", "dbo.Zipcode");
            DropForeignKey("dbo.Distillery", "CityID", "dbo.City");
            DropForeignKey("dbo.Brewery", "ZipCode", "dbo.Zipcode");
            DropForeignKey("dbo.Brewery", "CityID", "dbo.City");
            DropForeignKey("dbo.City", "StateID", "dbo.State");
            DropIndex("dbo.Winery", new[] { "ZipCode" });
            DropIndex("dbo.Winery", new[] { "CityID" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Distillery", new[] { "ZipCode" });
            DropIndex("dbo.Distillery", new[] { "CityID" });
            DropIndex("dbo.City", new[] { "StateID" });
            DropIndex("dbo.Brewery", new[] { "ZipCode" });
            DropIndex("dbo.Brewery", new[] { "CityID" });
            DropTable("dbo.Wine");
            DropTable("dbo.Winery");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Profile");
            DropTable("dbo.Liquor");
            DropTable("dbo.Distillery");
            DropTable("dbo.Zipcode");
            DropTable("dbo.State");
            DropTable("dbo.City");
            DropTable("dbo.Brewery");
            DropTable("dbo.Beer");
        }
    }
}
