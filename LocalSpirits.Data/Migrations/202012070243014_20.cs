namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notification", "SendersProfilePicture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notification", "SendersProfilePicture");
        }
    }
}
