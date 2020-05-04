using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Content.Api;
using Extensions;
using N0404.Entities;

namespace N0404.News
{
    public static class NewsItemFactory
    {
        private const string ContentPattern = "<p>(.*)<\\/p><a";
        
        public static INewsItem Create(N0404RssItem rssItem)
        {
            string description = Regex.Match(rssItem.Encoded, ContentPattern, RegexOptions.Singleline).Groups.LastOrDefault()?.Value;
            
            return new NewsItem(
                NewsSource.N0404,
                HttpUtility.HtmlDecode(rssItem.Title),
                HttpUtility.HtmlDecode(description),
                AuthorFactory.Create(),
                rssItem.Modified.ToDateTime(),
                rssItem.Link,
                rssItem.Enclosure.Url,
                null);
        }
    }
}