namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment", "SenderProfilePicture", c => c.String());
            AddColumn("dbo.Reply", "SenderProfilePicture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reply", "SenderProfilePicture");
            DropColumn("dbo.Comment", "SenderProfilePicture");
        }
    }
}
