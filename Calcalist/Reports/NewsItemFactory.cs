using Extensions;
using System.Linq;
using System.Text.RegularExpressions;
using Calcalist.Entities;
using Content.Api;

namespace Calcalist.Reports
{
    public static class NewsItemFactory
    {
        private const string ContentPattern = "<\\/div>(.*)";
        private const string ImagePattern = "<img\\s.*?src=(?:'|\")([^'\">]+)(?:'|\")";
        
        public static INewsItem Create(CalcalistRssItem rssItem)
        {
            string description = Regex.Match(rssItem.Description, ContentPattern).Groups.LastOrDefault()?.Value;
            string imageUrl = Regex.Match(rssItem.Description, ImagePattern).Groups.LastOrDefault()?.Value;
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