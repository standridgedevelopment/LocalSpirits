namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profile", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profile", "FullName");
        }
    }
}
