namespace NewsAggregator.Domain
{
    using System.Data.Entity;

    using NewsAggregator.Models;

    public class NewsContext : DbContext
    {
        public NewsContext()
            : base("NewsAggregator")
        {
        }

        public DbSet<NewsModel> News { get; set; }

        public DbSet<ExchangeRates> Rates { get; set; }
    }
}