namespace NewsAggregator.Controllers
{
    using System.Web.Mvc;

    using Models;

    public class HomeController : Controller
    {
        private readonly NewsContext newsContext = new NewsContext();

        public ActionResult Index()
        {
            return this.View(this.newsContext.NewsShorts);
        }
    }
}