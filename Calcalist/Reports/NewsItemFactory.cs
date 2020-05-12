using Extensions;
using System.Linq;
using System.Text.RegularExpressions;
using Calcalist.Entities;
using Content.Api;

namespace Calcalist.Reports
{
    public static class NewsItemFactory
    {
        private static readonly Regex ContentRegex = new Regex("<\\/div>(.*)");
        private static readonly Regex ImageRegex = new Regex("<img\\s.*?src=(?:'|\")([^'\">]+)(?:'|\")");
        
        public static INewsItem Create(CalcalistRssItem rssItem)
        {
            string description = ContentRegex.Match(rssItem.Description).Groups.LastOrDefault()?.Value;
            string imageUrl = ImageRegex.Match(rssItem.Description).Groups.LastOrDefault()?.Value;
            if (imageUrl == "")
            {
                imageUrl = null;
            }
            
            return new NewsItem(
                NewsSource.CalcalistReports,
                rssItem.Title,
                description,
                AuthorFactory.Create(rssItem.Author),
                rssItem.PublishDate.ToDateTime(),
                rssItem.Link,
                imageUrl,
                null);
        }
    }
}