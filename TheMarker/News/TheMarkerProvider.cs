using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Content.Api;
using Extensions;
using TheMarker.Entities;

namespace TheMarker.News
{
    public class TheMarkerProvider : ILatestNewsProvider
    {
        private readonly RssFeedProvider _rss;
        
        public TheMarkerProvider(HttpClient httpClient = null)
        {
            httpClient ??= new HttpClient();
            
            _rss = new RssFeedProvider(
                httpClient,
                "https://www.themarker.com/cmlink/1.144");
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
        
        private static TheMarkerRssFeed DeserializeXml(Stream xml)
        {
            var serializer = new XmlSerializer(typeof(TheMarkerRssFeed));

            return (TheMarkerRssFeed) serializer.Deserialize(xml);
        }
        
        private static IEnumerable<NewsItem> ToNewsItems(TheMarkerRssFeed feed)
        {
            return feed.Channel.Items
                .Select(NewsItemFactory.Create);
        }
    }
}