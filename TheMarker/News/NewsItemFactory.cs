using Extensions;
using System.Linq;
using System.Text.RegularExpressions;
using Content.Api;
using TheMarker.Entities;

namespace TheMarker.News
{
    public static class NewsItemFactory
    {
        private const string ContentPattern = "<\\/div>(.*)";
        private const string ImagePattern = "<img\\s.*?src=(?:'|\")([^'\">]+)(?:'|\")";
        
        public static NewsItem Create(TheMarkerRssItem rssItem)
        {
            string description = Regex.Match(rssItem.Description, ContentPattern, RegexOptions.Singleline).Groups.LastOrDefault()?.Value;
            string imageUrl = Regex.Match(rssItem.Description, ImagePattern).Groups.LastOrDefault()?.Value;
            
            return new NewsItem(
                NewsSource.TheMarker,
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