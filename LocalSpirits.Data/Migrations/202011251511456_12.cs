namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FriendRequest", "Sender", c => c.String());
            AddColumn("dbo.FriendRequest", "Reciever", c => c.String());
            AddColumn("dbo.FriendRequest", "SendersUsername", c => c.String());
            DropColumn("dbo.FriendRequest", "FullName");
            DropColumn("dbo.FriendRequest", "FriendsUsername");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FriendRequest", "FriendsUsername", c => c.String());
            AddColumn("dbo.FriendRequest", "FullName", c => c.String());
            DropColumn("dbo.FriendRequest", "SendersUsername");
            DropColumn("dbo.FriendRequest", "Reciever");
            DropColumn("dbo.FriendRequest", "Sender");
        }
    }
}
