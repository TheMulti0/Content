using Extensions;
using System.Linq;
using System.Text.RegularExpressions;
using Content.Api;
using Calcalist.Entities;

namespace Calcalist.News
{
    public static class NewsItemFactory
    {
        private static readonly Regex ContentRegex = new Regex("<\\/div>(.*)", RegexOptions.Singleline);
        private static readonly Regex ImageRegex = new Regex("<img\\s.*?src=(?:'|\")([^'\">]+)(?:'|\")");
        
        public static INewsItem Create(CalcalistRssItem rssItem)
        {
            string description = ContentRegex.Match(rssItem.Description).Groups.LastOrDefault()?.Value;
            string imageUrl = ImageRegex.Match(rssItem.Description).Groups.LastOrDefault()?.Value;
            
            return new NewsItem(
                NewsSource.Calcalist,
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