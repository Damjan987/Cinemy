namespace Cinemy.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedActorsFieldInMovieModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Actors", "Movie_Id", "dbo.Movies");
            DropIndex("dbo.Actors", new[] { "Movie_Id" });
            DropColumn("dbo.Actors", "Movie_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Actors", "Movie_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Actors", "Movie_Id");
            AddForeignKey("dbo.Actors", "Movie_Id", "dbo.Movies", "Id");
        }
    }
}
