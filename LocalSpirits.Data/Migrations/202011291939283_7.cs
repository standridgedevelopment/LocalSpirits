namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ActivityFeed", "LikedByUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ActivityFeed", "LikedByUser", c => c.Boolean(nullable: false));
        }
    }
}
