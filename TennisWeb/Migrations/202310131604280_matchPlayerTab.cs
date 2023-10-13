namespace TennisWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class matchPlayerTab : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MatchPlayers", "Ids", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MatchPlayers", "Ids", c => c.Int(nullable: false));
        }
    }
}
