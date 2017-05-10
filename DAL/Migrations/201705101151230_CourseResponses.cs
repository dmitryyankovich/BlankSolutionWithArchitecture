namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseResponses : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ResumeTags", newName: "TagResumes");
            DropPrimaryKey("dbo.TagResumes");
            CreateTable(
                "dbo.CourseResponses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        RefinementText = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        Course_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Course_Id)
                .Index(t => t.User_Id);
            
            AddPrimaryKey("dbo.TagResumes", new[] { "Tag_Id", "Resume_UserId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseResponses", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseResponses", "Course_Id", "dbo.Courses");
            DropIndex("dbo.CourseResponses", new[] { "User_Id" });
            DropIndex("dbo.CourseResponses", new[] { "Course_Id" });
            DropPrimaryKey("dbo.TagResumes");
            DropTable("dbo.CourseResponses");
            AddPrimaryKey("dbo.TagResumes", new[] { "Resume_UserId", "Tag_Id" });
            RenameTable(name: "dbo.TagResumes", newName: "ResumeTags");
        }
    }
}
