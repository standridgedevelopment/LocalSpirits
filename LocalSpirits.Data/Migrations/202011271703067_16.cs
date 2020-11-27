namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friend", "BusinessID", c => c.Int());
            CreateIndex("dbo.Friend", "BusinessID");
            AddForeignKey("dbo.Friend", "BusinessID", "dbo.Business", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friend", "BusinessID", "dbo.Business");
            DropIndex("dbo.Friend", new[] { "BusinessID" });
            DropColumn("dbo.Friend", "BusinessID");
        }
    }
}
