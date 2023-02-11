namespace Cinemy.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTransactionModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PAY_REQUEST_ID = c.String(),
                        AMOUNT = c.Int(nullable: false),
                        REFERENCE = c.String(),
                        TRANSACTION_STATUS = c.String(),
                        RESULT_CODE = c.Int(nullable: false),
                        RESULT_DESC = c.String(),
                        CUSTOMER_EMAIL_ADDRESS = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Transactions");
        }
    }
}
