namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityFeed",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Guid(),
                        Activity = c.Int(nullable: false),
                        Content = c.String(),
                        Created = c.DateTimeOffset(nullable: false, precision: 7),
                        Modified = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Profile", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Friend",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProfileID = c.Guid(),
                        FriendsID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Profile", t => t.FriendsID)
                .ForeignKey("dbo.Profile", t => t.ProfileID)
                .Index(t => t.ProfileID)
                .Index(t => t.FriendsID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friend", "ProfileID", "dbo.Profile");
            DropForeignKey("dbo.Friend", "FriendsID", "dbo.Profile");
            DropForeignKey("dbo.ActivityFeed", "UserID", "dbo.Profile");
            DropIndex("dbo.Friend", new[] { "FriendsID" });
            DropIndex("dbo.Friend", new[] { "ProfileID" });
            DropIndex("dbo.ActivityFeed", new[] { "UserID" });
            DropTable("dbo.Friend");
            DropTable("dbo.ActivityFeed");
        }
    }
}
