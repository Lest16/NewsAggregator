namespace NewsAggregator.Runner
{
    using NewsAggregator.Parsers;

    public class Program
    {
        public static void Main(string[] args)
        {
            var parser = new MordovMediaParser();
            parser.ParseNews();
        }
    }
}
