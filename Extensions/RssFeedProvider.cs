using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Content.Api;

namespace Extensions
{
    public class RssFeedProvider
    {
        private readonly HttpClient _client;
        private readonly string _rssFeed;

        public RssFeedProvider(
            HttpClient client,
            string rssFeed)
        {
            _client = client;
            _rssFeed = rssFeed;
        }
        
        public async Task<IEnumerable<NewsItem>> GetNews(
            CancellationToken cancellationToken,
            Func<Stream, IEnumerable<NewsItem>> deserializeItems)
        {
            Stream xml = await GetXmlAsync(cancellationToken);

            return deserializeItems(xml);
        }
        
        private async Task<Stream> GetXmlAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await _client.GetAsync(
                _rssFeed,
                cancellationToken);
            
            return await response.Content.ReadAsStreamAsync();
        }
        
        
    }
}