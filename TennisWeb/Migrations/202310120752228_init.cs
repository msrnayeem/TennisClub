namespace TennisWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CoachInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Description = c.String(),
                        UserId = c.Int(nullable: false),
                        Image = c.String(),
                        JoinDate = c.DateTime(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SlotId = c.Int(nullable: false),
                        CoachId = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Slots", t => t.SlotId, cascadeDelete: true)
                .ForeignKey("dbo.CoachInfoes", t => t.CoachId)
                .Index(t => t.SlotId)
                .Index(t => t.CoachId);
            
            CreateTable(
                "dbo.MatchPlayers",
                c => new
                    {
                        MatchId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MatchId, t.PlayerId })
                .ForeignKey("dbo.PlayerInfoes", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Matches", t => t.MatchId, cascadeDelete: true)
                .Index(t => t.MatchId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.PlayerInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        PlayingPosition = c.String(),
                        UserId = c.Int(nullable: false),
                        Image = c.String(),
                        JoinDate = c.DateTime(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        Status = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Slots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "CoachId", "dbo.CoachInfoes");
            DropForeignKey("dbo.Matches", "SlotId", "dbo.Slots");
            DropForeignKey("dbo.MatchPlayers", "MatchId", "dbo.Matches");
            DropForeignKey("dbo.PlayerInfoes", "UserId", "dbo.Users");
            DropForeignKey("dbo.CoachInfoes", "UserId", "dbo.Users");
            DropForeignKey("dbo.MatchPlayers", "PlayerId", "dbo.PlayerInfoes");
            DropIndex("dbo.PlayerInfoes", new[] { "UserId" });
            DropIndex("dbo.MatchPlayers", new[] { "PlayerId" });
            DropIndex("dbo.MatchPlayers", new[] { "MatchId" });
            DropIndex("dbo.Matches", new[] { "CoachId" });
            DropIndex("dbo.Matches", new[] { "SlotId" });
            DropIndex("dbo.CoachInfoes", new[] { "UserId" });
            DropTable("dbo.Slots");
            DropTable("dbo.Users");
            DropTable("dbo.PlayerInfoes");
            DropTable("dbo.MatchPlayers");
            DropTable("dbo.Matches");
            DropTable("dbo.CoachInfoes");
        }
    }
}
