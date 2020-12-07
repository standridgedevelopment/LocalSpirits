namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment", "SenderID", c => c.Guid());
            CreateIndex("dbo.Comment", "SenderID");
            AddForeignKey("dbo.Comment", "SenderID", "dbo.Profile", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "SenderID", "dbo.Profile");
            DropIndex("dbo.Comment", new[] { "SenderID" });
            DropColumn("dbo.Comment", "SenderID");
        }
    }
}
