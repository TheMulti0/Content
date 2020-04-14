using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Content.Api;

namespace Content.Controllers
{
    public class NewsService
    {
        private IPagedNewsProvider[] _pagedNewsProviders;

        public NewsService(IEnumerable<IPagedNewsProvider> pagedNewsProviders)
        {
            _pagedNewsProviders = pagedNewsProviders.ToArray();
        }

        public async Task<IEnumerable<NewsItem>> GetNews(int maxResults)
        {
            int providersCount = _pagedNewsProviders.Length;
            int itemsPerProvider = maxResults / providersCount;
            int stub = maxResults % providersCount;

            List<NewsItem> items = new List<NewsItem>();

            int index = 0;
            foreach (IPagedNewsProvider provider in _pagedNewsProviders)
            {
                int requestedItemsCount = itemsPerProvider;
                if (index + 1 == providersCount) // This is the last iteration
                {
                    requestedItemsCount += stub;
                }
                
                items.AddRange(
                    await provider.GetNews(requestedItemsCount));
                
                index++;
            }

            return items.OrderByDescending(item => item.Date); // Order from latest to earliest
        } 
    }
}