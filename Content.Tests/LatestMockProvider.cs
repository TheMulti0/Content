using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Content.Api;

namespace Content.Tests
{
    internal class LatestMockProvider : ILatestNewsProvider
    {
        private readonly int _results;

        public LatestMockProvider(int results)
        {
            _results = results;
        }

        public async Task<IEnumerable<NewsItem>> GetNews(
            CancellationToken cancellationToken = default)
        {
            return Enumerable
                .Range(1, _results)
                .Select(_ => MockNewsItemFactory.Create());
        }
    }
}