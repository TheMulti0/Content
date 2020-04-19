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
using Mako.News.Entities;

namespace Mako.News
{
    public class MakoProvider : ILatestNewsProvider
    {
        private readonly RssFeedProvider _rss;
        
        public MakoProvider(HttpClient httpClient = null)
        {
            httpClient ??= new HttpClient();
            
            _rss = new RssFeedProvider(
                httpClient,
                "http://rcs.mako.co.il/rss/31750a2610f26110VgnVCM1000005201000aRCRD.xml");
        }
        
        public Task<IEnumerable<NewsItem>> GetNews(
            CancellationToken cancellationToken = default)
        {
            return _rss.GetNews<MakoRssFeed>(
                cancellationToken,
                DeserializeItems);
        }

        public static IEnumerable<NewsItem> DeserializeItems(Stream xml)
        {
            return ToNewsItems(
                DeserializeXml<MakoRssFeed>(xml));
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
        
        private static IEnumerable<NewsItem> ToNewsItems(MakoRssFeed feed)
        {
            return feed.Channel.Items
                .Select(NewsItemFactory.Create);
        }
    }
}