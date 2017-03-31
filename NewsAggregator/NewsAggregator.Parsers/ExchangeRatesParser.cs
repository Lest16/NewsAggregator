namespace NewsAggregator.Parsers
{
    using AngleSharp;
    using AngleSharp.Dom;

    using NewsAggregator.Models;

    public class ExchangeRatesParser
    {
        public ExchangeRates ParseExchangeRates()
        {
            var page = this.GetPage("https://www.yandex.ru");
            var rates = page.QuerySelectorAll("span.inline-stocks__value_inner");
            var exchangeRates = new ExchangeRates
                                    {
                                        Usd = double.Parse(rates[0].TextContent),
                                        Eur = double.Parse(rates[1].TextContent)
                                    };
            return exchangeRates;
        }

        private IDocument GetPage(string address)
        {
            var document = BrowsingContext.New(Configuration.Default.WithDefaultLoader()).OpenAsync(address);
            document.Wait();
            return document.Result;
        }
    }
}