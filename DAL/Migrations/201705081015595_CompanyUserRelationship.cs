namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyUserRelationship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Company_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Company_Id");
            AddForeignKey("dbo.AspNetUsers", "Company_Id", "dbo.Companies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Company_Id", "dbo.Companies");
            DropIndex("dbo.AspNetUsers", new[] { "Company_Id" });
            DropColumn("dbo.AspNetUsers", "Company_Id");
        }
    }
}
