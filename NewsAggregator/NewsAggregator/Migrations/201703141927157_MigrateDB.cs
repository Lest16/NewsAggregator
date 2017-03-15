namespace NewsAggregator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsShorts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        TimeOccurrence = c.DateTime(nullable: false),
                        Source = c.String(),
                        Summary = c.String(),
                        PictureId = c.Int(nullable: false),
                        DetailDescription = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NewsShorts");
        }
    }
}
