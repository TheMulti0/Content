using System.Collections.Generic;
using System.Threading.Tasks;

namespace Content.Api
{
    public interface INewsProvider
    {
        Task<IEnumerable<NewsItem>> Get();
    }
}
