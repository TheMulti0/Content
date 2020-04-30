using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Content.Api;
using Extensions;
using Walla.Entities;

namespace Walla.Reports
{
    public class WallaReportsProvider : ILatestNewsProvider
    {
        private readonly RssFeedProvider _rss;
        
        public WallaReportsProvider(HttpClient httpClient = null)
        {
            httpClient ??= new HttpClient();
            
            _rss = new RssFeedProvider(
                httpClient,
                "https://rss.walla.co.il/feed/22");
        }
        
        public Task<IEnumerable<NewsItem>> GetNews(
            CancellationToken cancellationToken = default)
        {
            return _rss.GetNews(
                cancellationToken,
                DeserializeItems);
        }

        public static IEnumerable<NewsItem> DeserializeItems(Stream xml)
        {
            return ToNewsItems(
                DeserializeXml(xml));
        }
        
        private static WallaRssFeed DeserializeXml(Stream xml)
        {
            var serializer = new XmlSerializer(typeof(WallaRssFeed));
            
            return (WallaRssFeed) serializer.Deserialize(xml);
        }
        
        private static IEnumerable<NewsItem> ToNewsItems(WallaRssFeed feed)
        {
            return feed.Channel.Items
                .Select(NewsItemFactory.Create);
        }
    }

}