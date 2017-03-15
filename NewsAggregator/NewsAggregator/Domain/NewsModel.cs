namespace NewsAggregator.Domain
{
    using System;

    public class NewsModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime TimeOccurrence { get; set; }

        public string Source { get; set; }

        public string Summary { get; set; }

        public int PictureId { get; set; }

        public string DetailDescription { get; set; }
    }
}