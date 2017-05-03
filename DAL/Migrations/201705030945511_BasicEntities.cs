namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasicEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ContactFirstName = c.String(),
                        ContactLastName = c.String(),
                        ContactPhone = c.String(),
                        ContactEmail = c.String(),
                        City = c.String(),
                        Address = c.String(),
                        PathToAvatar = c.String(),
                        Benefits = c.String(),
                        IsCreationRequest = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Responsibilities = c.String(),
                        Requirements = c.String(),
                        Advantages = c.String(),
                        SalaryLevel = c.String(),
                        MinimalExpirience = c.String(),
                        Company_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Resumes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                        Cellphone = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        EducationLevel = c.Int(nullable: false),
                        EnglishLevel = c.Int(nullable: false),
                        SkillsLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagCourses",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Course_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.ResumeTags",
                c => new
                    {
                        Resume_Id = c.Int(nullable: false),
                        Tag_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Resume_Id, t.Tag_Id })
                .ForeignKey("dbo.Resumes", t => t.Resume_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .Index(t => t.Resume_Id)
                .Index(t => t.Tag_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.ResumeTags", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.ResumeTags", "Resume_Id", "dbo.Resumes");
            DropForeignKey("dbo.TagCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.TagCourses", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.ResumeTags", new[] { "Tag_Id" });
            DropIndex("dbo.ResumeTags", new[] { "Resume_Id" });
            DropIndex("dbo.TagCourses", new[] { "Course_Id" });
            DropIndex("dbo.TagCourses", new[] { "Tag_Id" });
            DropIndex("dbo.Courses", new[] { "Company_Id" });
            DropTable("dbo.ResumeTags");
            DropTable("dbo.TagCourses");
            DropTable("dbo.Resumes");
            DropTable("dbo.Tags");
            DropTable("dbo.Courses");
            DropTable("dbo.Companies");
        }
    }
}
