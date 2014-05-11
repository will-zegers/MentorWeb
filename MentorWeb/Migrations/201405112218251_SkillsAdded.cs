namespace MentorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkillsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        YearsExperience = c.Int(nullable: false),
                        ProfileID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Profiles", t => t.ProfileID)
                .Index(t => t.ProfileID);
            
            AddColumn("dbo.Profiles", "SkillID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skills", "ProfileID", "dbo.Profiles");
            DropIndex("dbo.Skills", new[] { "ProfileID" });
            DropColumn("dbo.Profiles", "SkillID");
            DropTable("dbo.Skills");
        }
    }
}
