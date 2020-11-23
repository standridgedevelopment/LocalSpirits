namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Business", "State");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Business", "State", c => c.String());
        }
    }
}
