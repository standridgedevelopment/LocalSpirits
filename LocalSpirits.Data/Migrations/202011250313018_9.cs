namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Friend", "Profile_ID", "dbo.Profile");
            DropIndex("dbo.Friend", new[] { "Profile_ID" });
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
            
            DropColumn("dbo.Friend", "Profile_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Friend", "Profile_ID", c => c.Guid());
            DropForeignKey("dbo.ActivityFeed", "UserID", "dbo.Profile");
            DropIndex("dbo.ActivityFeed", new[] { "UserID" });
            DropTable("dbo.ActivityFeed");
            CreateIndex("dbo.Friend", "Profile_ID");
            AddForeignKey("dbo.Friend", "Profile_ID", "dbo.Profile", "ID");
        }
    }
}
