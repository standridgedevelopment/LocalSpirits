namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Visited", "BusinessID", "dbo.Business");
            AddColumn("dbo.Visited", "Business_ID", c => c.Int());
            AddColumn("dbo.Visited", "Business_ID1", c => c.Int());
            CreateIndex("dbo.Visited", "Business_ID");
            CreateIndex("dbo.Visited", "Business_ID1");
            AddForeignKey("dbo.Visited", "Business_ID", "dbo.Business", "ID");
            AddForeignKey("dbo.Visited", "Business_ID1", "dbo.Business", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visited", "Business_ID1", "dbo.Business");
            DropForeignKey("dbo.Visited", "Business_ID", "dbo.Business");
            DropIndex("dbo.Visited", new[] { "Business_ID1" });
            DropIndex("dbo.Visited", new[] { "Business_ID" });
            DropColumn("dbo.Visited", "Business_ID1");
            DropColumn("dbo.Visited", "Business_ID");
            AddForeignKey("dbo.Visited", "BusinessID", "dbo.Business", "ID");
        }
    }
}
