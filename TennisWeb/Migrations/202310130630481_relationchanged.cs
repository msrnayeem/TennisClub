namespace TennisWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationchanged : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MatchPlayers", "PlayerId", "dbo.PlayerInfoes");
            AddColumn("dbo.MatchPlayers", "PlayerInfo_Id", c => c.Int());
            CreateIndex("dbo.MatchPlayers", "PlayerInfo_Id");
            AddForeignKey("dbo.MatchPlayers", "PlayerId", "dbo.PlayerInfoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MatchPlayers", "PlayerInfo_Id", "dbo.PlayerInfoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MatchPlayers", "PlayerInfo_Id", "dbo.PlayerInfoes");
            DropForeignKey("dbo.MatchPlayers", "PlayerId", "dbo.PlayerInfoes");
            DropIndex("dbo.MatchPlayers", new[] { "PlayerInfo_Id" });
            DropColumn("dbo.MatchPlayers", "PlayerInfo_Id");
            AddForeignKey("dbo.MatchPlayers", "PlayerId", "dbo.PlayerInfoes", "Id", cascadeDelete: true);
        }
    }
}
