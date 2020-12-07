namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notification", "CommentID", c => c.Int());
            CreateIndex("dbo.Notification", "CommentID");
            AddForeignKey("dbo.Notification", "CommentID", "dbo.Comment", "CommentID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notification", "CommentID", "dbo.Comment");
            DropIndex("dbo.Notification", new[] { "CommentID" });
            DropColumn("dbo.Notification", "CommentID");
        }
    }
}
