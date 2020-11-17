namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Business", "CityName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Business", "CityName");
        }
    }
}
