namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ActivityFeed", "UserID", "dbo.Profile");
            DropForeignKey("dbo.FriendRequest", "Profile_ID", "dbo.Profile");
            DropIndex("dbo.ActivityFeed", new[] { "UserID" });
            DropIndex("dbo.FriendRequest", new[] { "Profile_ID" });
            DropColumn("dbo.FriendRequest", "Profile_ID");
            DropTable("dbo.ActivityFeed");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.FriendRequest", "Profile_ID", c => c.Guid());
            CreateIndex("dbo.FriendRequest", "Profile_ID");
            CreateIndex("dbo.ActivityFeed", "UserID");
            AddForeignKey("dbo.FriendRequest", "Profile_ID", "dbo.Profile", "ID");
            AddForeignKey("dbo.ActivityFeed", "UserID", "dbo.Profile", "ID");
        }
    }
}
