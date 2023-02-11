namespace Cinemy.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedRatingFiledsToIntType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MovieRatings", "Rating", c => c.Int(nullable: false));
            AlterColumn("dbo.Movies", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Rating", c => c.Single(nullable: false));
            AlterColumn("dbo.MovieRatings", "Rating", c => c.Single(nullable: false));
        }
    }
}
