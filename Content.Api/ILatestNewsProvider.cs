using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Content.Api
{
    public interface ILatestNewsProvider
    {
        Task<IEnumerable<INewsItem>> GetNews(
            CancellationToken cancellationToken = default);
    }
}