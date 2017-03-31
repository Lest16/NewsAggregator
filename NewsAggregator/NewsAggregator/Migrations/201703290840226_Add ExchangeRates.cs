namespace NewsAggregator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExchangeRates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExchangeRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Usd = c.Double(nullable: false),
                        Eur = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExchangeRates");
        }
    }
}
