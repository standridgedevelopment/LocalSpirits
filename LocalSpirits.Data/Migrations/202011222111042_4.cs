namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Business", "ZipCode", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Business", "ZipCode", c => c.Int(nullable: false));
        }
    }
}
