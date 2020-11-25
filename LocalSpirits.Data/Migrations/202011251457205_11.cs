namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FriendRequest", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FriendRequest", "FullName");
        }
    }
}
