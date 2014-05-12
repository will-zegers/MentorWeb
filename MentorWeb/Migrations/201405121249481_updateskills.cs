namespace MentorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateskills : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Skills", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Skills", "UserName");
        }
    }
}
