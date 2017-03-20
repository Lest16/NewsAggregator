namespace NewsAggregator.Mapping
{
    using NewsAggregator.Domain;
    using NewsAggregator.Models;

    public class NewsMapper
    {
        public NewsShort MapNewsShort(NewsShort model, NewsModel entity)
        {
            model.Id = entity.Id;
            model.PictureHref = entity.PictureHref;
            model.Source = entity.Source;
            model.Summary = entity.Summary;
            model.TimeOccurrence = entity.TimeOccurrence;
            model.Title = entity.Title;
            return model;
        }

        public News MapNews(News model, NewsModel entity)
        {
            this.MapNewsShort(model, entity);
            model.DetailDescription = entity.DetailDescription;
            model.SoucreUrl = entity.SourceUrl;
            return model;
        }
    }
}