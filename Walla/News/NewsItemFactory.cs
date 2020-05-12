using System;
using System.Linq;
using System.Text.RegularExpressions;
using Content.Api;
using Extensions;
using Walla.Entities;

namespace Walla.News
{
    public static class NewsItemFactory
    {
        private static readonly Regex ContentRegex = new Regex("<\\/div>(.*)", RegexOptions.Singleline);
        private static readonly Regex ImageRegex = new Regex("<img\\s.*?src=(?:'|\")([^'\">]+)(?:'|\")");
        
        public static INewsItem Create(WallaRssItem rssItem)
        {
            string description = ContentRegex.Match(rssItem.Description).Groups.LastOrDefault()?.Value;
            string imageUrl = ImageRegex.Match(rssItem.Description).Groups.LastOrDefault()?.Value;
            
            return new NewsItem(
                NewsSource.Walla,
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