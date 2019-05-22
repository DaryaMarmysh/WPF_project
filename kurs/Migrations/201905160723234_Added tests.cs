namespace kurs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedtests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Counter = c.Int(nullable: false),
                        PointId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Points", t => t.PointId)
                .Index(t => t.PointId);
            
            CreateTable(
                "dbo.Points",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        User_Id = c.Int(),
                        CategoryId=c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id) 
                .Index(t => t.User_Id);
          
              



        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Points", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Answers", "PointId", "dbo.Points");
            DropIndex("dbo.Points", new[] { "User_Id" });
            DropIndex("dbo.Answers", new[] { "PointId" });
            DropTable("dbo.Points");
            DropTable("dbo.Answers");
        }
    }
}
