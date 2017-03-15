namespace NewsAggregator.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AngleSharp;
    using AngleSharp.Dom;

    using Domain;

    public class MordovMediaParser : IParser
    {
        public string SiteName => "МордовМедиа";

        public List<NewsModel> ParseNews()
        {
            var page = this.GetPage("http://www.mordovmedia.ru/news/");
            var news = page.QuerySelectorAll("div.news_item");
            var newsModelList = new List<NewsModel>();
            foreach (var itemNews in news)
            {
                var newsModel = new NewsModel();
                newsModel.Title = itemNews.QuerySelectorAll("a.news-title").First().TextContent;
                newsModel.Summary = itemNews.QuerySelectorAll("p").First().TextContent;
                newsModel.PictureHref = itemNews.QuerySelectorAll("img").First().Attributes["src"].Value;
                newsModel.TimeOccurrence = this.ParseDate(itemNews.QuerySelectorAll("span.date").First().TextContent);
                newsModel.Source = this.SiteName;
                var sourceUrl = itemNews.QuerySelectorAll("a.news-title").First().Attributes["href"].Value;
                newsModel.SourceUrl = sourceUrl;
                var detailedTextPage = this.GetPage(sourceUrl);
                newsModel.DetailDescription = detailedTextPage.QuerySelectorAll("div.news-text").First().TextContent;
                newsModelList.Add(newsModel);
            }
            return newsModelList;
        }

        private IDocument GetPage(string address)
        { 
            var document = BrowsingContext.New(Configuration.Default.WithDefaultLoader()).OpenAsync(address);
            document.Wait();
            return document.Result;
        }

        private DateTime ParseDate(string dateString)
        {
            var splitDate = dateString.Split(' ');
            var splitTime = splitDate[0].Split(':');
            if (splitDate[1] == "Сегодня")
            {
                return new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    int.Parse(splitTime[0]),
                    int.Parse(splitTime[1]),
                    0);
            }
            if (splitDate[1] == "Вчера")
            {
                return new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day - 1,
                    int.Parse(splitTime[0]),
                    int.Parse(splitTime[1]),
                    0);
            }

            var splitOldDate = splitDate[1].Split('.');
            return new DateTime(int.Parse(splitOldDate[2]), int.Parse(splitOldDate[1]), int.Parse(splitOldDate[0]),
                        int.Parse(splitTime[0]), int.Parse(splitTime[1]), 0);
        }
    }
}