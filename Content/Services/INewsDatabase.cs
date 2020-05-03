using System.Collections.Generic;
using System.Threading.Tasks;
using Content.Models;

namespace Content.Services
{
    public interface INewsDatabase
    {
        Task<IEnumerable<NewsItemEntity>> GetAsync();

        Task AddAsync(NewsItemEntity item);

        Task AddRangeAsync(IEnumerable<NewsItemEntity> items);

        Task RemoveAsync(NewsItemEntity item);
    }
}