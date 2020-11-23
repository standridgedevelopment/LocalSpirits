namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Beer", "BreweryID", "dbo.Business");
            DropIndex("dbo.Beer", new[] { "BreweryID" });
            DropTable("dbo.Beer");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Beer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        BreweryID = c.Int(nullable: false),
                        Aroma = c.String(),
                        Taste = c.String(),
                        ABV = c.Int(nullable: false),
                        Hops = c.String(),
                        Package = c.String(),
                        KegsAvailable = c.Boolean(nullable: false),
                        Availability = c.String(),
                        Website = c.String(),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.Beer", "BreweryID");
            AddForeignKey("dbo.Beer", "BreweryID", "dbo.Business", "ID", cascadeDelete: true);
        }
    }
}
