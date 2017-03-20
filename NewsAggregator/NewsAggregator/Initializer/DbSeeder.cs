namespace NewsAggregator.Initializer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NewsAggregator.Domain;

    public class DbSeeder
    {
        private readonly NewsContext newsContext = new NewsContext();

        public void AddNews(List<NewsModel> newsModels)
        {
            var dateTimeLatestNews = new DateTime();
            if (this.newsContext.News.Count() != 0)
            {
                dateTimeLatestNews = this.newsContext.News.ToList().Max(x => x.TimeOccurrence);
            }

            this.newsContext.News.AddRange(newsModels.Where(x => x.TimeOccurrence > dateTimeLatestNews));
            this.newsContext.SaveChanges();
        }
    }
}