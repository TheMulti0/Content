using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Content.Api
{
    public interface ILatestNewsProvider
    {
        Task<IEnumerable<NewsItem>> GetNews(
            CancellationToken cancellationToken = default);
    }
}