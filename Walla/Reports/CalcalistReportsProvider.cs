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
                DeserializeXml<CalcalistRssFeed>(xml));
        }
        
        private static T DeserializeXml<T>(Stream xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            
            return (T) serializer.Deserialize(xml);
        }
        
        private static IEnumerable<NewsItem> ToNewsItems(CalcalistRssFeed feed)
        {
            return feed.Channel.Items
                .Select(NewsItemFactory.Create);
        }
    }

}