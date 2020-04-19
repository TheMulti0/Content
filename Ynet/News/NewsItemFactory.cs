using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Content.Api;
using Ynet.News.Entities;

namespace Ynet.News
{
    public static class NewsItemFactory
    {
        private const string ContentPattern = "<\\/div>(.*)";
        private const string ImagePattern = "<img\\s.*?src=(?:'|\")([^'\">]+)(?:'|\")";
        
        public static NewsItem Create(YnetRssItem rssItem)
        {
            // Date example: Mon, 24 Feb 2020 09:44:00 +0200
            DateTime date = DateTime.Parse(rssItem.PublishDate);

            string description = Regex.Match(rssItem.Description, ContentPattern).Groups.LastOrDefault()?.Value;
            string imageUrl = Regex.Match(rssItem.Description, ImagePattern).Groups.LastOrDefault()?.Value;
            
            return new NewsItem(
                NewsSource.Ynet,
                rssItem.Title,
                description,
                AuthorFactory.Create(),
                date,
                rssItem.Link,
                imageUrl,
                null);
        }
    }
}