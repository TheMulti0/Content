using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Content.Api;
using Content.Models;

namespace Content.Services
{
    public class InMemoryNewsDatabase : INewsDatabase
    {
        private readonly ConcurrentDictionary<NewsItemInfo, NewsItemEntity> _entities = new ConcurrentDictionary<NewsItemInfo, NewsItemEntity>();
        
        public async Task<IEnumerable<NewsItemEntity>> GetAsync() => _entities.Values;
        
        public async Task<IEnumerable<NewsItemEntity>> GetAsync(int maxResults, NewsSource[] excludedSources)
        {
            return _entities.Values
                .Where(item => excludedSources.All(source => item.Source != source))
                .Take(maxResults);
        }

        public async Task AddAsync(NewsItemEntity item)
        {
            _entities.TryAdd(CreateKey(item), item);
        }

        public async Task AddRangeAsync(IEnumerable<NewsItemEntity> items)
        {
            foreach (NewsItemEntity item in items)
            {
                await AddAsync(item);
            }
        }

        public async Task RemoveAsync(NewsItemEntity item)
        {
            _entities.TryRemove(CreateKey(item), out item);
        }

        private static NewsItemInfo CreateKey(INewsItem item)
            => new NewsItemInfo(item);
    }
}