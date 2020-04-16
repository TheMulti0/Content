using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Content.Api;

namespace Content.Tests
{
    internal class PagedMockProvider : IPagedNewsProvider
    {
        public int MaximumItemsPerPage { get; } = 100;
        
        public async Task<IEnumerable<NewsItem>> GetNews(
            int maxResults,
            int firstPage = 0,
            CancellationToken cancellationToken = default)
        {
            return Enumerable
                .Range(1, maxResults)
                .Select(_ => MockNewsItemFactory.Create());
        }
    }
}