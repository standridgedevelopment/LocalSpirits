namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FriendRequest", "FriendsID", "dbo.Profile");
            DropForeignKey("dbo.Friend", "FriendsID", "dbo.Profile");
            DropIndex("dbo.FriendRequest", new[] { "FriendsID" });
            DropIndex("dbo.Friend", new[] { "FriendsID" });
            AddColumn("dbo.FriendRequest", "FriendsUsername", c => c.String());
            AddColumn("dbo.Friend", "FriendsUsername", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Friend", "FriendsUsername");
            DropColumn("dbo.FriendRequest", "FriendsUsername");
            CreateIndex("dbo.Friend", "FriendsID");
            CreateIndex("dbo.FriendRequest", "FriendsID");
            AddForeignKey("dbo.Friend", "FriendsID", "dbo.Profile", "ID");
            AddForeignKey("dbo.FriendRequest", "FriendsID", "dbo.Profile", "ID");
        }
    }
}
