namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Visited", "EventID", "dbo.Event");
            DropIndex("dbo.Visited", new[] { "EventID" });
            AlterColumn("dbo.Visited", "EventID", c => c.Int());
            CreateIndex("dbo.Visited", "EventID");
            AddForeignKey("dbo.Visited", "EventID", "dbo.Event", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visited", "EventID", "dbo.Event");
            DropIndex("dbo.Visited", new[] { "EventID" });
            AlterColumn("dbo.Visited", "EventID", c => c.Int(nullable: false));
            CreateIndex("dbo.Visited", "EventID");
            AddForeignKey("dbo.Visited", "EventID", "dbo.Event", "id", cascadeDelete: true);
        }
    }
}
