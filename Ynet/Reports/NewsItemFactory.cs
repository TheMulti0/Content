using Extensions;
using System.Linq;
using System.Text.RegularExpressions;
using Content.Api;
using Ynet.Entities;

namespace Ynet.Reports
{
    public static class NewsItemFactory
    {
        private const string ContentPattern = "<\\/div>(.*)";
        private const string ImagePattern = "<img\\s.*?src=(?:'|\")([^'\">]+)(?:'|\")";
        
        public static NewsItem Create(YnetRssItem rssItem)
        {
            string description = Regex.Match(rssItem.Description, ContentPattern).Groups.LastOrDefault()?.Value;
            string imageUrl = Regex.Match(rssItem.Description, ImagePattern).Groups.LastOrDefault()?.Value;
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