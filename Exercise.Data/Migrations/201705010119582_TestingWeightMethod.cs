namespace Exercise.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestingWeightMethod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workout", "OwnerWeight", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Workout", "OwnerWeight");
        }
    }
}
