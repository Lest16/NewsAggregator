namespace NewsAggregator.Models
{
    using System.Data.Entity;

    public class NewsContext : DbContext
    {
        public DbSet<News> News { get; set; }

        public DbSet<NewsShort> NewsShorts { get; set; }
    }
}