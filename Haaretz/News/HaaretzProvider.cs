using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Content.Api;
using Extensions;
using Haaretz.Entities;

namespace Haaretz.News
{
    public class HaaretzProvider : ILatestNewsProvider
    {
        private readonly RssFeedProvider _rss;
        
        public HaaretzProvider(HttpClient httpClient = null)
        {
            httpClient ??= new HttpClient();
            
            _rss = new RssFeedProvider(
                httpClient,
                "https://www.haaretz.co.il/cmlink/1.1617539");
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
        
        private static HaaretzRssFeed DeserializeXml(Stream xml)
        {
            var serializer = new XmlSerializer(typeof(HaaretzRssFeed));

            return (HaaretzRssFeed) serializer.Deserialize(xml);
        }
        
        private static IEnumerable<NewsItem> ToNewsItems(HaaretzRssFeed feed)
        {
            return feed.Channel.Items
                .Select(NewsItemFactory.Create);
        }
    }

}