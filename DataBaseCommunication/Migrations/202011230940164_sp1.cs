namespace DataBaseCommunication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sp1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Biography", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "Biography");
        }
    }
}
