namespace DataBaseCommunication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mt6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "BanReason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "BanReason");
        }
    }
}
