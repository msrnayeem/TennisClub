namespace TennisWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSlotModelTimeType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Slots", "Start", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Slots", "End", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Slots", "End", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Slots", "Start", c => c.DateTime(nullable: false));
        }
    }
}
