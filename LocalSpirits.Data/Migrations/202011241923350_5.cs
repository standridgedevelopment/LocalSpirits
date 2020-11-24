namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendRequest",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProfileID = c.Guid(),
                        FriendsID = c.Guid(),
                        Created = c.DateTimeOffset(nullable: false, precision: 7),
                        Profile_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Profile", t => t.FriendsID)
                .ForeignKey("dbo.Profile", t => t.ProfileID)
                .ForeignKey("dbo.Profile", t => t.Profile_ID)
                .Index(t => t.ProfileID)
                .Index(t => t.FriendsID)
                .Index(t => t.Profile_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendRequest", "Profile_ID", "dbo.Profile");
            DropForeignKey("dbo.FriendRequest", "ProfileID", "dbo.Profile");
            DropForeignKey("dbo.FriendRequest", "FriendsID", "dbo.Profile");
            DropIndex("dbo.FriendRequest", new[] { "Profile_ID" });
            DropIndex("dbo.FriendRequest", new[] { "FriendsID" });
            DropIndex("dbo.FriendRequest", new[] { "ProfileID" });
            DropTable("dbo.FriendRequest");
        }
    }
}
