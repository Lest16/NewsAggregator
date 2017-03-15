namespace NewsAggregator.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Domain;
    using Mapping;
    using Models;

    public class HomeController : Controller
    {
        private readonly NewsContext newsContext = new NewsContext();

        private readonly NewsMapper mapper = new NewsMapper();

        public ActionResult Index()
        {
            var newsShorts = this.newsContext.News.Select(news => this.mapper.MapNewsShort(new NewsShort(), news)).ToList();
            return this.View(newsShorts);
        }
    }
}