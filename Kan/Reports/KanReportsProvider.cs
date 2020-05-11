using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Content.Api;
using TwitterScraper;

namespace Kan.Reports
{
    public class KanReportsProvider : ILatestNewsProvider
    {
        private const string Username = "@kann_news";
        private ITwitter _twitter;

        public KanReportsProvider(HttpClient client = null)
        {
            _twitter = new Twitter(client);
        }

        public async Task<IEnumerable<INewsItem>> GetNews(CancellationToken cancellationToken = default)
        {
            IEnumerable<Tweet> tweets = await _twitter.GetTweets(Username, token: cancellationToken);
            
            return tweets.Select(NewsItemFactory.Create);
        }
    }
}