namespace NewsAggregator.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AngleSharp;
    using AngleSharp.Dom;

    using NewsAggregator.Domain;

    public class PgParser: IParser
    {
        public string SiteName => "ProГород";

        private readonly Dictionary<string, int> month = new Dictionary<string, int>
                                                    {
                                                        { "января", 01},
                                                        { "февраля", 02},
                                                        { "марта", 03},
                                                        { "апреля", 04},
                                                        { "мая", 05},
                                                        { "июня", 06}
                                                    };

        public List<NewsModel> ParseNews()
        {
            var page = this.GetPage("http://pg13.ru/articles");
            var news = page.QuerySelectorAll("div.article-list__item");
            var newsModelList = new List<NewsModel>();
            foreach (var itemNews in news)
            {
                var newsModel = new NewsModel();
                newsModel.Title = itemNews.QuerySelectorAll("a.link_nodecor").First().TextContent.Trim();
                var rawHref = itemNews.QuerySelectorAll("a.article-list__item-image").First().Attributes["style"].Value;
                newsModel.PictureHref = rawHref.Substring(rawHref.IndexOf("(", StringComparison.Ordinal) + 1, 
                    rawHref.IndexOf(")", StringComparison.Ordinal) - rawHref.IndexOf("(", StringComparison.Ordinal) - 1);
                newsModel.TimeOccurrence = this.ParseDate(itemNews.QuerySelectorAll("span.article-list__item-date").First().TextContent);
                newsModel.Source = this.SiteName;
                var sourceUrl = "http://pg13.ru" + itemNews.QuerySelectorAll("a.article-list__item-image").First().Attributes["href"].Value;
                newsModel.SourceUrl = sourceUrl;
                var detailedTextPage = this.GetPage(sourceUrl);
                newsModel.Summary = detailedTextPage.QuerySelectorAll("div.article__lead-content").First().TextContent.Trim();
                var description = String.Empty;
                var pTags = detailedTextPage.QuerySelectorAll("div.article__main-content").First().QuerySelectorAll("p");
                for (var i=0; i < pTags.Length - 1; i++)
                {
                    description += pTags[i].TextContent;
                }
                newsModel.DetailDescription = description;
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
            var split = dateString.Trim().Split(',');
            var splitDate = split[0].Split(' ');
            var splitTime = split[1].Split(':');

            return new DateTime(int.Parse(splitDate[2]), this.month[splitDate[1]], int.Parse(splitDate[0]),
                        int.Parse(splitTime[0]), int.Parse(splitTime[1]), 0);
        }
    }
}