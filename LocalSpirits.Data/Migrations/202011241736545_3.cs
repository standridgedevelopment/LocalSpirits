namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profile", "Username", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profile", "Username");
        }
    }
}
