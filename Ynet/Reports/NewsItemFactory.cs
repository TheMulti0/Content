using Extensions;
using System.Linq;
using System.Text.RegularExpressions;
using Content.Api;
using Ynet.Entities;

namespace Ynet.Reports
{
    public static class NewsItemFactory
    {
        private static readonly Regex ContentRegex = new Regex("<\\/div>(.*)", RegexOptions.Singleline);
        private static readonly Regex ImageRegex = new Regex("<img\\s.*?src=(?:'|\")([^'\">]+)(?:'|\")");
        
        public static INewsItem Create(YnetRssItem rssItem)
        {
            string description = ContentRegex.Match(rssItem.Description).Groups.LastOrDefault()?.Value;
            string imageUrl = ImageRegex.Match(rssItem.Description).Groups.LastOrDefault()?.Value;
            if (imageUrl == "")
            {
                imageUrl = null;
            }
            
            return new NewsItem(
                NewsSource.YnetReports,
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