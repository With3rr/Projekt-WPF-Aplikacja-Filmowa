namespace DataBaseCommunication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mt4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "PersonalCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "PersonalCode");
        }
    }
}
