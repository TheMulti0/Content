using System;
using System.Linq;
using System.Text.RegularExpressions;
using Content.Api;
using Walla.Entities;

namespace Walla.News
{
    public static class NewsItemFactory
    {
        private const string ContentPattern = "<\\/div>(.*)";
        private const string ImagePattern = "<img\\s.*?src=(?:'|\")([^'\">]+)(?:'|\")";
        
        public static NewsItem Create(CalcalistRssItem rssItem)
        {
            // Date example: Mon, 24 Feb 2020 09:44:00 +0200
            DateTime date = DateTime.Parse(rssItem.PublishDate);

            string description = Regex.Match(rssItem.Description, ContentPattern, RegexOptions.Singleline).Groups.LastOrDefault()?.Value;
            string imageUrl = Regex.Match(rssItem.Description, ImagePattern).Groups.LastOrDefault()?.Value;
            
            return new NewsItem(
                NewsSource.Calcalist,
                rssItem.Title,
                description,
                AuthorFactory.Create(rssItem.Author),
                date,
                rssItem.Link,
                imageUrl,
                null);
        }
    }
}