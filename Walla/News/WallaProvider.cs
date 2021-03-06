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

namespace Walla.News
{
    public class WallaProvider : ILatestNewsProvider
    {
        private readonly RssFeedProvider _rss;
        
        public WallaProvider(HttpClient httpClient = null)
        {
            httpClient ??= new HttpClient();
            
            _rss = new RssFeedProvider(
                httpClient,
                "https://rss.walla.co.il/feed/1?type=main");
        }
        
        public Task<IEnumerable<INewsItem>> GetNews(
            CancellationToken cancellationToken = default)
        {
            return _rss.GetNews(
                cancellationToken,
                DeserializeItems);
        }

        public static IEnumerable<INewsItem> DeserializeItems(Stream xml)
        {
            return ToNewsItems(
                DeserializeXml(xml));
        }
        
        private static WallaRssFeed DeserializeXml(Stream xml)
        {
            var serializer = new XmlSerializer(typeof(WallaRssFeed));

            return (WallaRssFeed) serializer.Deserialize(xml);
        }
        
        private static IEnumerable<INewsItem> ToNewsItems(WallaRssFeed feed)
        {
            return feed.Channel.Items
                .Select(NewsItemFactory.Create);
        }
    }

}