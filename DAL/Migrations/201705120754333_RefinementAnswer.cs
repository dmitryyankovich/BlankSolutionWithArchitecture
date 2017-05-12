namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefinementAnswer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseResponses", "RefinementAnswerText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourseResponses", "RefinementAnswerText");
        }
    }
}
