using System.Linq;
using System.Text.RegularExpressions;
using Content.Api;
using Walla.Entities;
using Extensions;

namespace Walla.Reports
{
    public static class NewsItemFactory
    {
        private static readonly Regex ContentRegex = new Regex("<br\\/>(.*)<\\/p");
        
        public static INewsItem Create(WallaRssItem rssItem)
        {
            string description = ContentRegex.Match(rssItem.Description).Groups.LastOrDefault()?.Value;
            
            return new NewsItem(
                NewsSource.WallaReports,
                rssItem.Title,
                description,
                AuthorFactory.Create(),
                rssItem.PublishDate.ToDateTime(),
                rssItem.Link,
                null,
                null);
        }
    }
}