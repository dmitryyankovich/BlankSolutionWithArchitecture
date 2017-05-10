namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResumeKey : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ResumeTags", name: "Resume_Id", newName: "Resume_UserId");
            RenameColumn(table: "dbo.Resumes", name: "Id", newName: "UserId");
            RenameIndex(table: "dbo.Resumes", name: "IX_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.ResumeTags", name: "IX_Resume_Id", newName: "IX_Resume_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ResumeTags", name: "IX_Resume_UserId", newName: "IX_Resume_Id");
            RenameIndex(table: "dbo.Resumes", name: "IX_UserId", newName: "IX_Id");
            RenameColumn(table: "dbo.Resumes", name: "UserId", newName: "Id");
            RenameColumn(table: "dbo.ResumeTags", name: "Resume_UserId", newName: "Resume_Id");
        }
    }
}
