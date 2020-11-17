namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Brewery", newName: "Business");
            AddColumn("dbo.Business", "TypeOfEstablishment", c => c.String(nullable: false));
            AlterColumn("dbo.Business", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Business", "CityName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Business", "CityName", c => c.String());
            AlterColumn("dbo.Business", "Name", c => c.String());
            DropColumn("dbo.Business", "TypeOfEstablishment");
            RenameTable(name: "dbo.Business", newName: "Brewery");
        }
    }
}
