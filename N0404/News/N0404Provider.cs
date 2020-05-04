using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Content.Api;
using Extensions;
using N0404.Entities;

namespace N0404.News
{
    public class N0404Provider : ILatestNewsProvider
    {
        private readonly RssFeedProvider _rss;
        
        public N0404Provider(HttpClient httpClient = null)
        {
            httpClient ??= new HttpClient();
            
            _rss = new RssFeedProvider(
                httpClient,
                "https://www.0404.co.il/?call_custom_simple_rss=1&csrp_cat=14");
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
        
        private static N0404RssFeed DeserializeXml(Stream xml)
        {
            var serializer = new XmlSerializer(typeof(N0404RssFeed));

            return (N0404RssFeed) serializer.Deserialize(xml);
        }
        
        private static IEnumerable<INewsItem> ToNewsItems(N0404RssFeed feed)
        {
            return feed.Channel.Items
                .Select(NewsItemFactory.Create);
        }
    }

}