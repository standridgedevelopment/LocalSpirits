namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Visited",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Guid(),
                        BusinessID = c.Int(),
                        EventID = c.Int(),
                        AddToFavorites = c.Boolean(nullable: false),
                        AddToCalendar = c.Boolean(nullable: false),
                        Rating = c.Int(nullable: false),
                        Review = c.String(),
                        Profile_ID = c.Guid(),
                        Profile_ID1 = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Business", t => t.BusinessID)
                .ForeignKey("dbo.Event", t => t.EventID)
                .ForeignKey("dbo.Profile", t => t.Profile_ID)
                .ForeignKey("dbo.Profile", t => t.Profile_ID1)
                .ForeignKey("dbo.Profile", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.BusinessID)
                .Index(t => t.EventID)
                .Index(t => t.Profile_ID)
                .Index(t => t.Profile_ID1);
            
            AddColumn("dbo.Event", "Profile_ID", c => c.Guid());
            CreateIndex("dbo.Event", "Profile_ID");
            AddForeignKey("dbo.Event", "Profile_ID", "dbo.Profile", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visited", "UserID", "dbo.Profile");
            DropForeignKey("dbo.Visited", "Profile_ID1", "dbo.Profile");
            DropForeignKey("dbo.Event", "Profile_ID", "dbo.Profile");
            DropForeignKey("dbo.Visited", "Profile_ID", "dbo.Profile");
            DropForeignKey("dbo.Visited", "EventID", "dbo.Event");
            DropForeignKey("dbo.Visited", "BusinessID", "dbo.Business");
            DropIndex("dbo.Visited", new[] { "Profile_ID1" });
            DropIndex("dbo.Visited", new[] { "Profile_ID" });
            DropIndex("dbo.Visited", new[] { "EventID" });
            DropIndex("dbo.Visited", new[] { "BusinessID" });
            DropIndex("dbo.Visited", new[] { "UserID" });
            DropIndex("dbo.Event", new[] { "Profile_ID" });
            DropColumn("dbo.Event", "Profile_ID");
            DropTable("dbo.Visited");
        }
    }
}
