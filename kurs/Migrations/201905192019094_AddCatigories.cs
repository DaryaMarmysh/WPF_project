namespace kurs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCatigories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Points", "CategoryId", c => c.Int());
            CreateIndex("dbo.Points", "CategoryId");
            AddForeignKey("dbo.Points", "CategoryId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Points", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Points", new[] { "CategoryId" });
            DropColumn("dbo.Points", "CategoryId");
            DropTable("dbo.Categories");
        }
    }
}
