namespace NewsAggregator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddfieldsourceUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewsModels", "SourceUrl", c => c.String());
            AddColumn("dbo.NewsModels", "PictureHref", c => c.String());
            DropColumn("dbo.NewsModels", "PictureId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NewsModels", "PictureId", c => c.Int(nullable: false));
            DropColumn("dbo.NewsModels", "PictureHref");
            DropColumn("dbo.NewsModels", "SourceUrl");
        }
    }
}
