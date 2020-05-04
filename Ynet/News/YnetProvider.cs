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
using Ynet.Entities;

namespace Ynet.News
{
    public class YnetProvider : ILatestNewsProvider
    {
        private readonly RssFeedProvider _rss;
        
        public YnetProvider(HttpClient httpClient = null)
        {
            httpClient ??= new HttpClient();
            
            _rss = new RssFeedProvider(
                httpClient,
                "http://www.ynet.co.il/Integration/StoryRss2.xml");
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
                DeserializeXml<YnetRssFeed>(xml));
        }
        
        private static T DeserializeXml<T>(Stream xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            
            StreamReader reader = new StreamReader( xml );
            string text = reader.ReadToEnd();
            text = text.Substring(39); // Remove the root element (<xml>) since it has no closing tag
            byte[] byteArray = Encoding.UTF8.GetBytes( text );
            var stream = new MemoryStream( byteArray );
            
            return (T) serializer.Deserialize(stream);
        }
        
        private static IEnumerable<INewsItem> ToNewsItems(YnetRssFeed feed)
        {
            return feed.Channel.Items
                .Select(NewsItemFactory.Create);
        }
    }

}