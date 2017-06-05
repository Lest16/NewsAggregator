namespace NewsAggregator.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    using AngleSharp;
    using AngleSharp.Dom;

    using NewsAggregator.Domain;

    public class MordovMediaParser : IParser
    {
        public string SiteName => "МордовМедиа";

        public List<NewsModel> ParseNews()
        {
            var page = this.GetPage("http://www.mordovmedia.ru/news/");
            var srcList = this.ParseRss();
            var news = page.QuerySelectorAll("article.news_item");
            var newsModelList = new List<NewsModel>();
            foreach (var itemNews in news)
            {
                var newsModel = new NewsModel();
                newsModel.Title = itemNews.QuerySelectorAll("a.news-title, a.news_title").First().TextContent;
                newsModel.Summary = itemNews.QuerySelectorAll("p").First().TextContent;
                newsModel.TimeOccurrence = this.ParseDate(itemNews.QuerySelectorAll("span.date, div.date").First().TextContent);
                newsModel.Source = this.SiteName;
                var sourceUrl = itemNews.QuerySelectorAll("a.news-title, a.news_title").First().Attributes["href"].Value;
                newsModel.SourceUrl = sourceUrl;
                var detailedTextPage = this.GetPage(sourceUrl);
                newsModel.DetailDescription = detailedTextPage.QuerySelectorAll("div.news-text").First().TextContent;
                newsModelList.Add(newsModel);
            }

            newsModelList.Sort((x, y) => y.TimeOccurrence.CompareTo(x.TimeOccurrence));
            for (var i = 0; i < newsModelList.Count; i++)
            {
                newsModelList[i].PictureHref = srcList[i];
            }

            return newsModelList;
        }

        private List<string> ParseRss()
        {
            var rssXmlDoc = new XmlDocument();
            rssXmlDoc.Load("http://www.mordovmedia.ru/news/rss/");
            var rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");
            var srcList = new List<string>();
            foreach (XmlNode rssNode in rssNodes)
            {
                var rssSubNode = rssNode.SelectSingleNode("description");
                var description = rssSubNode?.FirstChild.Value;
                var src = description?.Substring(
                    description.LastIndexOf("src=", StringComparison.Ordinal) + 5,
                    description.LastIndexOf(" style", StringComparison.Ordinal) - 11);
                srcList.Add(src);
            }

            return srcList;
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
            var splitTime = splitDate[1].Split(':');
            if (splitDate[0] == "Сегодня")
            {
                return new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    int.Parse(splitTime[0]),
                    int.Parse(splitTime[1]),
                    0);
            }
            if (splitDate[0] == "Вчера")
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