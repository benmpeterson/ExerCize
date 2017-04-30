namespace Exercise.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedIntsToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Workout", "Duration", c => c.Double(nullable: false));
            AlterColumn("dbo.Workout", "CaloriesBurned", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Workout", "CaloriesBurned", c => c.Int(nullable: false));
            AlterColumn("dbo.Workout", "Duration", c => c.Int(nullable: false));
        }
    }
}
