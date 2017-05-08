namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResumeUserLink : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ResumeTags", "Resume_Id", "dbo.Resumes");
            DropPrimaryKey("dbo.Resumes");
            AlterColumn("dbo.Resumes", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Resumes", "Id");
            CreateIndex("dbo.Resumes", "Id");
            AddForeignKey("dbo.Resumes", "Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ResumeTags", "Resume_Id", "dbo.Resumes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResumeTags", "Resume_Id", "dbo.Resumes");
            DropForeignKey("dbo.Resumes", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Resumes", new[] { "Id" });
            DropPrimaryKey("dbo.Resumes");
            AlterColumn("dbo.Resumes", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Resumes", "Id");
            AddForeignKey("dbo.ResumeTags", "Resume_Id", "dbo.Resumes", "Id", cascadeDelete: true);
        }
    }
}
