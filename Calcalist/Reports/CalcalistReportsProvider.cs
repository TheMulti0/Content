using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Calcalist.Entities;
using Content.Api;
using Extensions;

namespace Calcalist.Reports
{
    public class CalcalistReportsProvider : ILatestNewsProvider
    {
        private readonly RssFeedProvider _rss;
        
        public CalcalistReportsProvider(HttpClient httpClient = null)
        {
            httpClient ??= new HttpClient();
            
            _rss = new RssFeedProvider(
                httpClient,
                "http://www.ynet.co.il/Integration/StoryRss1854.xml");
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
        
        private static CalcalistRssFeed DeserializeXml (Stream xml)
        {
            var serializer = new XmlSerializer(typeof(CalcalistRssFeed));
            
            return (CalcalistRssFeed) serializer.Deserialize(xml);
        }
        
        private static IEnumerable<INewsItem> ToNewsItems(CalcalistRssFeed feed)
        {
            return feed.Channel.Items
                .Select(NewsItemFactory.Create);
        }
    }

}