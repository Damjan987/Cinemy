namespace Cinemy.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMovieRatingModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieRatings",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Movie = c.String(),
                        Rating = c.Double(),
                        User = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MovieRatings");
        }
    }
}
