namespace DataBaseCommunication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mt7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "BanType", c => c.String());
            DropColumn("dbo.Accounts", "Active");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "Active", c => c.Boolean(nullable: false));
            DropColumn("dbo.Accounts", "BanType");
        }
    }
}
