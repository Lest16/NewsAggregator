﻿namespace NewsAggregator.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Mapping;
    using Models;

    using NewsAggregator.Domain;

    public class HomeController : Controller
    {
        private readonly NewsContext newsContext = new NewsContext();

        private readonly NewsMapper mapper = new NewsMapper();

        public ActionResult Index()
        {
            var newsShorts = new List<NewsShort>();
            foreach (var news in this.newsContext.News)
            {
                newsShorts.Add(this.mapper.MapNewsShort(new NewsShort(), news));
            }

            newsShorts.Sort((x, y) => y.TimeOccurrence.CompareTo(x.TimeOccurrence));
            return this.View(newsShorts);
        }

        public ActionResult Detailed(int id)
        {
            return this.View(this.mapper.MapNews(new News(), this.newsContext.News.ToList().Single(x => x.Id == id)));
        }

        public ActionResult TodayNews()
        {
            var newsShorts = new List<NewsShort>();
            foreach (var news in this.newsContext.News.Where(x => x.TimeOccurrence.Day == DateTime.Now.Day))        
            {
                newsShorts.Add(this.mapper.MapNewsShort(new NewsShort(), news));
            }

            newsShorts.Sort((x, y) => y.TimeOccurrence.CompareTo(x.TimeOccurrence));
            return this.View(newsShorts);
        }

        public ActionResult LastThreeDays()
        {
            var newsShorts = new List<NewsShort>();
            foreach (var news in this.newsContext.News.Where(x => x.TimeOccurrence.Day > DateTime.Now.Day - 3 && x.TimeOccurrence.Month == DateTime.Now.Month))
            {
                newsShorts.Add(this.mapper.MapNewsShort(new NewsShort(), news));
            }

            newsShorts.Sort((x, y) => y.TimeOccurrence.CompareTo(x.TimeOccurrence));
            return this.View(newsShorts);
        }

        public ActionResult GetExchangeRates()
        {
            return this.PartialView(this.newsContext.Rates.First());
        }
    }
}