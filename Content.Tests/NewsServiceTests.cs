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
            IEnumerable<NewsItem> items = await newsService.GetNews(maxResults, new NewsSource[0]);
            Assert.Equal(maxResults, items.Count());
        }
        
        [Fact]
        public async Task TestZeroProviders()
        {
            var newsService = new NewsService(
                new ILatestNewsProvider[0],
                new IPagedNewsProvider[0]);

            var maxResults = 20;
            IEnumerable<NewsItem> items = await newsService.GetNews(maxResults, new NewsSource[0]);
            Assert.Empty(items);
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
            IEnumerable<NewsItem> items = await newsService.GetNews(maxResults, new NewsSource[0]);
            Assert.Equal(maxResults, items.Count());
        }
    }
}