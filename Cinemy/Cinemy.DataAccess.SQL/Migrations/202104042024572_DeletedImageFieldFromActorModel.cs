namespace Cinemy.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedImageFieldFromActorModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "Image", c => c.String());
        }
        
        public override void Down()
        {
        }
    }
}
