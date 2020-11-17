namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Business", "ZipCode", "dbo.Zipcode");
            DropForeignKey("dbo.Distillery", "ZipCode", "dbo.Zipcode");
            DropIndex("dbo.Business", new[] { "ZipCode" });
            DropIndex("dbo.Distillery", new[] { "ZipCode" });
            AddColumn("dbo.Business", "State", c => c.String());
            AddColumn("dbo.Distillery", "State", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Distillery", "State");
            DropColumn("dbo.Business", "State");
            CreateIndex("dbo.Distillery", "ZipCode");
            CreateIndex("dbo.Business", "ZipCode");
            AddForeignKey("dbo.Distillery", "ZipCode", "dbo.Zipcode", "ZipCode", cascadeDelete: true);
            AddForeignKey("dbo.Business", "ZipCode", "dbo.Zipcode", "ZipCode", cascadeDelete: true);
        }
    }
}
