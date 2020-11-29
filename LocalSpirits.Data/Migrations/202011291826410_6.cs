namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Like",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Guid(),
                        ActivityFeedID = c.Int(),
                        Liked = c.Boolean(nullable: false),
                        Created = c.DateTimeOffset(nullable: false, precision: 7),
                        Modified = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ActivityFeed", t => t.ActivityFeedID)
                .ForeignKey("dbo.Profile", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.ActivityFeedID);
            
            AddColumn("dbo.ActivityFeed", "LikedByUser", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Like", "UserID", "dbo.Profile");
            DropForeignKey("dbo.Like", "ActivityFeedID", "dbo.ActivityFeed");
            DropIndex("dbo.Like", new[] { "ActivityFeedID" });
            DropIndex("dbo.Like", new[] { "UserID" });
            DropColumn("dbo.ActivityFeed", "LikedByUser");
            DropTable("dbo.Like");
        }
    }
}
