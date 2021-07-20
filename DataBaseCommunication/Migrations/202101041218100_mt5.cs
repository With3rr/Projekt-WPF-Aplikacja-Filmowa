namespace DataBaseCommunication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mt5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Accounts", "BanTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "BanTime");
            DropColumn("dbo.Accounts", "Active");
        }
    }
}
