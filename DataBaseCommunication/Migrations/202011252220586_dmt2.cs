namespace DataBaseCommunication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dmt2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "RankName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "RankName");
        }
    }
}
