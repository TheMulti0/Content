using System.Linq;
using System.Text.RegularExpressions;
using Content.Api;
using Walla.Entities;
using Extensions;

namespace Walla.Reports
{
    public static class NewsItemFactory
    {
        private const string ContentPattern = "<br\\/>(.*)<\\/p";
        
        public static NewsItem Create(WallaRssItem rssItem)
        {
            string description = Regex.Match(rssItem.Description, ContentPattern).Groups.LastOrDefault()?.Value;
            
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