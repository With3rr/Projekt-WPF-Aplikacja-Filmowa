namespace DataBaseCommunication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mt71 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "AccounttType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "AccounttType");
        }
    }
}
