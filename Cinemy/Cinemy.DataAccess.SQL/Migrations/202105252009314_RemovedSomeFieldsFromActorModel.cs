namespace Cinemy.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedSomeFieldsFromActorModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Actors", "Age");
            DropColumn("dbo.Actors", "Image");
            DropColumn("dbo.Actors", "Sex");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Actors", "Sex", c => c.Int(nullable: false));
            AddColumn("dbo.Actors", "Image", c => c.String());
            AddColumn("dbo.Actors", "Age", c => c.Int(nullable: false));
        }
    }
}
