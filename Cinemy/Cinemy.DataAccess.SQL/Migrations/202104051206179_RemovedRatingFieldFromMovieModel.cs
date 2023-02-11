namespace Cinemy.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRatingFieldFromMovieModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movies", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Rating", c => c.Single(nullable: false));
        }
    }
}
