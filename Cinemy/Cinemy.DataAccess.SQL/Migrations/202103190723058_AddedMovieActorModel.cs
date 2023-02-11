namespace Cinemy.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMovieActorModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieActors",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        movie = c.String(),
                        actor = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MovieActors");
        }
    }
}
