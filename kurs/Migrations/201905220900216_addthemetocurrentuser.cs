namespace kurs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addthemetocurrentuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Theme", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Theme");
        }
    }
}
