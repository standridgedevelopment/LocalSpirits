namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reply",
                c => new
                    {
                        ReplyID = c.Int(nullable: false, identity: true),
                        CommentID = c.Int(),
                        ReplyContent = c.String(),
                        SenderFullName = c.String(),
                        SenderUsername = c.String(),
                        Created = c.DateTimeOffset(nullable: false, precision: 7),
                        Modified = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.ReplyID)
                .ForeignKey("dbo.Comment", t => t.CommentID)
                .Index(t => t.CommentID);
            
            AddColumn("dbo.Comment", "WhenPosted", c => c.String());
            AddColumn("dbo.Comment", "HowLongAgo", c => c.String());
            AddColumn("dbo.Like", "CommentID", c => c.Int());
            CreateIndex("dbo.Like", "CommentID");
            AddForeignKey("dbo.Like", "CommentID", "dbo.Comment", "CommentID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reply", "CommentID", "dbo.Comment");
            DropForeignKey("dbo.Like", "CommentID", "dbo.Comment");
            DropIndex("dbo.Reply", new[] { "CommentID" });
            DropIndex("dbo.Like", new[] { "CommentID" });
            DropColumn("dbo.Like", "CommentID");
            DropColumn("dbo.Comment", "HowLongAgo");
            DropColumn("dbo.Comment", "WhenPosted");
            DropTable("dbo.Reply");
        }
    }
}
