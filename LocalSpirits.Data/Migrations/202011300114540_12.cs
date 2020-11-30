namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notification", "ActivityFeedID", c => c.Int());
            CreateIndex("dbo.Notification", "ActivityFeedID");
            AddForeignKey("dbo.Notification", "ActivityFeedID", "dbo.ActivityFeed", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notification", "ActivityFeedID", "dbo.ActivityFeed");
            DropIndex("dbo.Notification", new[] { "ActivityFeedID" });
            DropColumn("dbo.Notification", "ActivityFeedID");
        }
    }
}
