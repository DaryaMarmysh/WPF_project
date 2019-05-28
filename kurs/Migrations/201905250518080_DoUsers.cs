namespace kurs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoUsers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserPoints", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserPoints", "Point_Id", "dbo.Points");
            DropIndex("dbo.UserPoints", new[] { "User_Id" });
            DropIndex("dbo.UserPoints", new[] { "Point_Id" });
            AddColumn("dbo.Points", "User_Id", c => c.Int());
            AddColumn("dbo.Users", "TestTitle_Id", c => c.Int());
            CreateIndex("dbo.Points", "User_Id");
            CreateIndex("dbo.Users", "TestTitle_Id");
            AddForeignKey("dbo.Points", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "TestTitle_Id", "dbo.TestTitles", "Id");
            DropTable("dbo.UserPoints");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserPoints",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Point_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Point_Id });
            
            DropForeignKey("dbo.Users", "TestTitle_Id", "dbo.TestTitles");
            DropForeignKey("dbo.Points", "User_Id", "dbo.Users");
            DropIndex("dbo.Users", new[] { "TestTitle_Id" });
            DropIndex("dbo.Points", new[] { "User_Id" });
            DropColumn("dbo.Users", "TestTitle_Id");
            DropColumn("dbo.Points", "User_Id");
            CreateIndex("dbo.UserPoints", "Point_Id");
            CreateIndex("dbo.UserPoints", "User_Id");
            AddForeignKey("dbo.UserPoints", "Point_Id", "dbo.Points", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserPoints", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
