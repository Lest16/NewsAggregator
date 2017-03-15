namespace NewsAggregator.Parsers
{
    using System.Collections.Generic;

    using Domain;

    public interface IParser
    {
        string SiteName { get; }

        List<NewsModel> ParseNews();
    }
}