namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "ThirdPartyWebsite", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Event", "ThirdPartyWebsite");
        }
    }
}
