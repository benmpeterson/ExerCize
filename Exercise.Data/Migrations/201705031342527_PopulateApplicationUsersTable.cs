namespace Exercise.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateApplicationUsersTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO dbo.ApplicationUser (Id, Sex, Age, Weight, Email, EmailConfirmed, PasswordHash, UserName) VALUES (3,'Male', 20, 160, 'user2@user.com', 'False', 'Password1!', 'user2@user.com')");
        }
        
        public override void Down()
        {
        }
    }
}
