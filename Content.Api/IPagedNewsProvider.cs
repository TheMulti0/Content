using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Content.Api
{
    public interface IPagedNewsProvider
    {
        int MaximumItemsPerPage { get; }
        
        Task<IEnumerable<NewsItem>> GetNews(
            int maxResults,
            int firstPage = 0, // Zero based
            CancellationToken cancellationToken = default);
    }
}
