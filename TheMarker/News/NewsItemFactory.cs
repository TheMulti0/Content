using Extensions;
using System.Linq;
using System.Text.RegularExpressions;
using Content.Api;
using TheMarker.Entities;

namespace TheMarker.News
{
    public static class NewsItemFactory
    {
        private static readonly Regex ContentRegex = new Regex("<\\/div>(.*)", RegexOptions.Singleline);
        
        public static INewsItem Create(TheMarkerRssItem rssItem)
        {
            string description = ContentRegex.Match(rssItem.Description).Groups.LastOrDefault()?.Value;
            
            return new NewsItem(
                NewsSource.TheMarker,
                rssItem.Title,
                description,
                AuthorFactory.Create(rssItem.Author),
                rssItem.PublishDate.ToDateTime(),
                rssItem.Link,
                rssItem.Enclosure.Url,
                null);
        }
    }
}