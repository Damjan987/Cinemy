namespace Cinemy.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedMovieUserModel : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.MovieUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MovieUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        MovieId = c.String(),
                        UserId = c.String(),
                        Image = c.String(),
                        Name = c.String(),
                        GenreId = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rating = c.Int(nullable: false),
                        IsBought = c.Boolean(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
