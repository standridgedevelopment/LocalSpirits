namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        FeedID = c.Int(),
                        CommentContent = c.String(),
                        SenderFullName = c.String(),
                        SenderUsername = c.String(),
                        Created = c.DateTimeOffset(nullable: false, precision: 7),
                        Modified = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.ActivityFeed", t => t.FeedID)
                .Index(t => t.FeedID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "FeedID", "dbo.ActivityFeed");
            DropIndex("dbo.Comment", new[] { "FeedID" });
            DropTable("dbo.Comment");
        }
    }
}
