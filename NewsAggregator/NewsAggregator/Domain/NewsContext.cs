namespace NewsAggregator.Domain
{
    using System.Data.Entity;

    public class NewsContext : DbContext
    {
        public NewsContext()
            : base("NewsAggregator")
        {
        }

        public DbSet<NewsModel> News { get; set; }
    }
}