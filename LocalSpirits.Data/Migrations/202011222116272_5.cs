namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Business", "LiveMusic");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Business", "LiveMusic", c => c.Boolean(nullable: false));
        }
    }
}
