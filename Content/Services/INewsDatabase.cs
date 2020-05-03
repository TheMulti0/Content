using System.Collections.Generic;
using System.Threading.Tasks;
using Content.Api;
using Content.Models;

namespace Content.Services
{
    public interface INewsDatabase
    {
        Task<IEnumerable<NewsItemEntity>> GetAsync();
        
        Task<IEnumerable<NewsItemEntity>> GetAsync(int maxResults, NewsSource[] excludedSources);

        Task AddAsync(NewsItemEntity item);

        Task AddRangeAsync(IEnumerable<NewsItemEntity> items);

        Task RemoveAsync(NewsItemEntity item);
    }
}