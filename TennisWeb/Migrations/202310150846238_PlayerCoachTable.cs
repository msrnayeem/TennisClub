namespace TennisWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlayerCoachTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PlayerInfoes", new[] { "UserId" });
            AlterColumn("dbo.CoachInfoes", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.CoachInfoes", "Address", c => c.String(maxLength: 255));
            AlterColumn("dbo.CoachInfoes", "Phone", c => c.String(maxLength: 20));
            AlterColumn("dbo.CoachInfoes", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.PlayerInfoes", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.PlayerInfoes", "Address", c => c.String(maxLength: 255));
            AlterColumn("dbo.PlayerInfoes", "Phone", c => c.String(maxLength: 20));
            AlterColumn("dbo.PlayerInfoes", "PlayingPosition", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.PlayerInfoes", "DateOfBirth", c => c.DateTime());
            CreateIndex("dbo.PlayerInfoes", "UserId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.PlayerInfoes", new[] { "UserId" });
            AlterColumn("dbo.PlayerInfoes", "DateOfBirth", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PlayerInfoes", "PlayingPosition", c => c.String());
            AlterColumn("dbo.PlayerInfoes", "Phone", c => c.String());
            AlterColumn("dbo.PlayerInfoes", "Address", c => c.String());
            AlterColumn("dbo.PlayerInfoes", "Name", c => c.String());
            AlterColumn("dbo.CoachInfoes", "Description", c => c.String());
            AlterColumn("dbo.CoachInfoes", "Phone", c => c.String());
            AlterColumn("dbo.CoachInfoes", "Address", c => c.String());
            AlterColumn("dbo.CoachInfoes", "Name", c => c.String());
            CreateIndex("dbo.PlayerInfoes", "UserId");
        }
    }
}
