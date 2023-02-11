namespace Cinemy.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMovieUserModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovieUsers", "MovieId", c => c.String());
            AddColumn("dbo.MovieUsers", "UserId", c => c.String());
            AddColumn("dbo.MovieUsers", "Image", c => c.String());
            AddColumn("dbo.MovieUsers", "Name", c => c.String());
            AddColumn("dbo.MovieUsers", "GenreId", c => c.String());
            AddColumn("dbo.MovieUsers", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MovieUsers", "Rating", c => c.Int(nullable: false));
            AddColumn("dbo.MovieUsers", "IsBought", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovieUsers", "IsBought");
            DropColumn("dbo.MovieUsers", "Rating");
            DropColumn("dbo.MovieUsers", "Price");
            DropColumn("dbo.MovieUsers", "GenreId");
            DropColumn("dbo.MovieUsers", "Name");
            DropColumn("dbo.MovieUsers", "Image");
            DropColumn("dbo.MovieUsers", "UserId");
            DropColumn("dbo.MovieUsers", "MovieId");
        }
    }
}
