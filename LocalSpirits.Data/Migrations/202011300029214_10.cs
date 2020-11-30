namespace LocalSpirits.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Profile_ID = c.Guid(),
                        SenderFullName = c.String(),
                        senderUsername = c.String(),
                        Recieved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Profile", t => t.Profile_ID)
                .Index(t => t.Profile_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notification", "Profile_ID", "dbo.Profile");
            DropIndex("dbo.Notification", new[] { "Profile_ID" });
            DropTable("dbo.Notification");
        }
    }
}
