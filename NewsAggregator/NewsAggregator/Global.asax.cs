namespace NewsAggregator
{
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Initializer;
    using Parsers;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var parser = new MordovMediaParser();
            var dbSeeder = new DbSeeder();
            dbSeeder.AddNews(parser.ParseNews());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
