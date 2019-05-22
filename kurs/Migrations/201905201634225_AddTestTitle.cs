namespace kurs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTestTitle : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Points", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Points", new[] { "CategoryId" });
            CreateTable(
                "dbo.TestTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            AddColumn("dbo.Points", "TestTitleId", c => c.Int());
            CreateIndex("dbo.Points", "TestTitleId");
            AddForeignKey("dbo.Points", "TestTitleId", "dbo.TestTitles", "Id");
            DropColumn("dbo.Points", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Points", "CategoryId", c => c.Int());
            DropForeignKey("dbo.Points", "TestTitleId", "dbo.TestTitles");
            DropForeignKey("dbo.TestTitles", "CategoryId", "dbo.Categories");
            DropIndex("dbo.TestTitles", new[] { "CategoryId" });
            DropIndex("dbo.Points", new[] { "TestTitleId" });
            DropColumn("dbo.Points", "TestTitleId");
            DropTable("dbo.TestTitles");
            CreateIndex("dbo.Points", "CategoryId");
            AddForeignKey("dbo.Points", "CategoryId", "dbo.Categories", "Id");
        }
    }
}
