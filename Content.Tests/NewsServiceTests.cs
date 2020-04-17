using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Content.Api;
using Content.Services;
using Xunit;

namespace Content.Tests
{
    public class NewsServiceTests
    {
        [Fact]
        public async Task Test()
        {
            var paged = new PagedMockProvider();
            var latest = new LatestMockProvider(10);

            var newsService = new NewsService(
                new[] { latest },
                new[] { paged });

            var maxResults = 20;
            IEnumerable<NewsItem> items = await newsService.GetNews(maxResults, new NewsProviderType[0]);
            Assert.Equal(maxResults, items.Count());
        }
        
        [Fact]
        public async Task TestOddNumber()
        {
            var paged = new PagedMockProvider();
            var latest = new LatestMockProvider(10);

            var newsService = new NewsService(
                new[] { latest },
                new[] { paged });

            var maxResults = 33;
            IEnumerable<NewsItem> items = await newsService.GetNews(maxResults, new NewsProviderType[0]);
            Assert.Equal(maxResults, items.Count());
        }
    }
}