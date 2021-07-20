namespace DataBaseCommunication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.Notifications", new[] { "Account_Id" });
            DropTable("dbo.Notifications");
        }
    }
}
