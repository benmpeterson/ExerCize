namespace Exercise.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSexAgeWeightToRegisterView : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUser", "Sex", c => c.String());
            AddColumn("dbo.ApplicationUser", "Age", c => c.Double(nullable: false));
            AddColumn("dbo.ApplicationUser", "Weight", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUser", "Weight");
            DropColumn("dbo.ApplicationUser", "Age");
            DropColumn("dbo.ApplicationUser", "Sex");
        }
    }
}
