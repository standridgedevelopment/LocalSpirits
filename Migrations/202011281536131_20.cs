namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Business",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    TypeOfEstablishment = c.String(nullable: false),
                    CityID = c.Int(),
                    ZipCode = c.Int(),
                    Hours = c.String(),
                    PhoneNumber = c.String(),
                    Website = c.String(),
                    Rating = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.City", t => t.CityID)
                .Index(t => t.CityID);

            CreateTable(
                "dbo.City",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    State = c.String(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Event",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    title = c.String(),
                    BusinessID = c.Int(nullable: false),
                    TypeOfEvent = c.String(),
                    State = c.String(),
                    City = c.String(),
                    start = c.String(),
                    end = c.String(),
                    DaysOfWeekInput = c.String(),
                    startRecur = c.String(),
                    endRecur = c.String(),
                    url = c.String(),
                    ThirdPartyWebsite = c.String(),
                    color = c.String(),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Business", t => t.BusinessID, cascadeDelete: true)
                .Index(t => t.BusinessID);

            CreateTable(
                "dbo.Visited",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Profile_ID = c.Guid(),
                    BusinessID = c.Int(),
                    EventID = c.Int(),
                    AddToFavorites = c.Boolean(nullable: false),
                    AddToCalendar = c.Boolean(nullable: false),
                    Rating = c.Int(nullable: false),
                    Review = c.String(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Business", t => t.BusinessID)
                .ForeignKey("dbo.Event", t => t.EventID)
                .ForeignKey("dbo.Profile", t => t.Profile_ID)
                .Index(t => t.Profile_ID)
                .Index(t => t.BusinessID)
                .Index(t => t.EventID);

            CreateTable(
                "dbo.Profile",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    FirstName = c.String(nullable: false),
                    LastName = c.String(nullable: false),
                    City = c.String(nullable: false),
                    State = c.Int(nullable: false),
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
                "dbo.State",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.ID);

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

        }

        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Visited", "Profile_ID", "dbo.Profile");
            DropForeignKey("dbo.Visited", "EventID", "dbo.Event");
            DropForeignKey("dbo.Visited", "BusinessID", "dbo.Business");
            DropForeignKey("dbo.Event", "BusinessID", "dbo.Business");
            DropForeignKey("dbo.Business", "CityID", "dbo.City");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Visited", new[] { "EventID" });
            DropIndex("dbo.Visited", new[] { "BusinessID" });
            DropIndex("dbo.Visited", new[] { "Profile_ID" });
            DropIndex("dbo.Event", new[] { "BusinessID" });
            DropIndex("dbo.Business", new[] { "CityID" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.State");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Profile");
            DropTable("dbo.Visited");
            DropTable("dbo.Event");
            DropTable("dbo.City");
            DropTable("dbo.Business");
        }
    }
}
