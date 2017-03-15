namespace NewsAggregator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        TimeOccurrence = c.DateTime(nullable: false),
                        Source = c.String(),
                        Summary = c.String(),
                        PictureId = c.Int(nullable: false),
                        DetailDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NewsModels");
        }
    }
}
