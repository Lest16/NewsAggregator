namespace NewsAggregator.Initializer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NewsAggregator.Domain;
    using NewsAggregator.Models;

    public class DbSeeder
    {
        private readonly NewsContext newsContext = new NewsContext();

        public void AddNews(List<NewsModel> newsModels)
        {
            var dateTimeLatestNews = new DateTime();
            if (this.newsContext.News.Count() != 0)
            {
                dateTimeLatestNews = this.newsContext.News.ToList().Where(x => x.Source == newsModels[0].Source).Max(x => x.TimeOccurrence);
            }

            this.newsContext.News.AddRange(newsModels.Where(x => x.TimeOccurrence > dateTimeLatestNews));
            this.newsContext.SaveChanges();
        }

        public void AddRates(ExchangeRates rates)
        {
            this.newsContext.Rates.Add(rates);
            this.newsContext.SaveChanges();
        }
    }
}