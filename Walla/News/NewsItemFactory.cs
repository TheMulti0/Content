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
        private const string ContentPattern = "<br\\/>(.*)<\\/p";
        private const string ImagePattern = "<img\\s.*?src=(?:'|\")([^'\">]+)(?:'|\")";
        
        public static NewsItem Create(WallaRssItem rssItem)
        {
            string description = Regex.Match(rssItem.Description, ContentPattern, RegexOptions.Singleline).Groups.LastOrDefault()?.Value;
            string imageUrl = Regex.Match(rssItem.Description, ImagePattern).Groups.LastOrDefault()?.Value;
            
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