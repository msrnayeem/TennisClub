namespace TennisWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class matchPlayerTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MatchPlayers", "Ids", c => c.Int(nullable: false));
            DropColumn("dbo.MatchPlayers", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MatchPlayers", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.MatchPlayers", "Ids");
        }
    }
}
