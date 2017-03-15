namespace NewsAggregator.Models
{
    using System;

    public class NewsShort
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime TimeOccurrence { get; set; }

        public string Source { get; set; }

        public string Summary { get; set; }

        public string PictureHref { get; set; }
    }
}