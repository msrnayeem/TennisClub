namespace TennisWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class matchplayernew : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.MatchPlayers");
            AddColumn("dbo.MatchPlayers", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.MatchPlayers", "Id");
            DropColumn("dbo.MatchPlayers", "Ids");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MatchPlayers", "Ids", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.MatchPlayers");
            DropColumn("dbo.MatchPlayers", "Id");
            AddPrimaryKey("dbo.MatchPlayers", new[] { "MatchId", "PlayerId" });
        }
    }
}
