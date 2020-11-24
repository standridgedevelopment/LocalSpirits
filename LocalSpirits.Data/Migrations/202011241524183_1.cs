namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friend", "Profile_ID", c => c.Guid());
            CreateIndex("dbo.Friend", "Profile_ID");
            AddForeignKey("dbo.Friend", "Profile_ID", "dbo.Profile", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friend", "Profile_ID", "dbo.Profile");
            DropIndex("dbo.Friend", new[] { "Profile_ID" });
            DropColumn("dbo.Friend", "Profile_ID");
        }
    }
}
