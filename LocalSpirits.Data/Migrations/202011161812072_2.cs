namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Brewery", "ZipCode", "dbo.Zipcode");
            DropForeignKey("dbo.Distillery", "ZipCode", "dbo.Zipcode");
            DropIndex("dbo.Brewery", new[] { "ZipCode" });
            DropIndex("dbo.Distillery", new[] { "ZipCode" });
            AddColumn("dbo.Brewery", "State", c => c.String());
            AddColumn("dbo.Distillery", "State", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Distillery", "State");
            DropColumn("dbo.Brewery", "State");
            CreateIndex("dbo.Distillery", "ZipCode");
            CreateIndex("dbo.Brewery", "ZipCode");
            AddForeignKey("dbo.Distillery", "ZipCode", "dbo.Zipcode", "ZipCode", cascadeDelete: true);
            AddForeignKey("dbo.Brewery", "ZipCode", "dbo.Zipcode", "ZipCode", cascadeDelete: true);
        }
    }
}
