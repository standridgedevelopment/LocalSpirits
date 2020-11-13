namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.City", "State", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.City", "State", c => c.Int(nullable: false));
        }
    }
}
