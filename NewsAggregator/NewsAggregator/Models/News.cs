namespace NewsAggregator.Models
{
    public class News : NewsShort
    {
        public string DetailDescription { get; set; }

        public string SoucreUrl { get; set; }
    }
}