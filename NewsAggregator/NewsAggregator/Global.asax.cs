namespace NewsAggregator
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Initializer;
    using Parsers;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var parsers = new List<IParser> { new MordovMediaParser(), new PgParser() };
            var dbSeeder = new DbSeeder();
            foreach (var parser in parsers)
            {
                dbSeeder.AddNews(parser.ParseNews());
            }

            var parserRates = new ExchangeRatesParser();
            dbSeeder.AddRates(parserRates.ParseExchangeRates());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
