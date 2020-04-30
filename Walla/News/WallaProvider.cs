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
                "https://www.calcalist.co.il/GeneralRSS/0,16335,L-8,00.xml");
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
        
        private static CalcalistRssFeed DeserializeXml(Stream xml)
        {
            var serializer = new XmlSerializer(typeof(CalcalistRssFeed));

            return (CalcalistRssFeed) serializer.Deserialize(xml);
        }
        
        private static IEnumerable<NewsItem> ToNewsItems(CalcalistRssFeed feed)
        {
            return feed.Channel.Items
                .Select(NewsItemFactory.Create);
        }
    }

}