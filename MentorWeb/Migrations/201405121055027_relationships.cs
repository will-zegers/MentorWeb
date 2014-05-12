namespace MentorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationships : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Relationships",
                c => new
                    {
                        rID = c.Int(nullable: false, identity: true),
                        mentorID = c.String(),
                        menteeID = c.String(),
                        accepted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.rID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Relationships");
        }
    }
}
