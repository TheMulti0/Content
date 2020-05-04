using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Content.Api;
using Extensions;
using Ynet.Entities;

namespace Ynet.News
{
    public static class NewsItemFactory
    {
        private const string ContentPattern = "<\\/div>(.*)";
        private const string ImagePattern = "<img\\s.*?src=(?:'|\")([^'\">]+)(?:'|\")";
        
        public static INewsItem Create(YnetRssItem rssItem)
        {
            string description = Regex.Match(rssItem.Description, ContentPattern).Groups.LastOrDefault()?.Value;
            string imageUrl = Regex.Match(rssItem.Description, ImagePattern).Groups.LastOrDefault()?.Value;
            
            return new NewsItem(
                NewsSource.Ynet,
                rssItem.Title,
                description,
                AuthorFactory.Create(),
                rssItem.PublishDate.ToDateTime(),
                rssItem.Link,
                imageUrl,
                null);
        }
    }
}