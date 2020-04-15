using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Content.Api;
using Content.Models;

namespace Content.Services
{
    public class NewsService
    {
        private readonly IPagedNewsProvider[] _pagedNewsProviders;

        public NewsService(IEnumerable<IPagedNewsProvider> pagedNewsProviders)
        {
            _pagedNewsProviders = pagedNewsProviders.ToArray();
        }

        public async Task<IEnumerable<NewsItem>> GetNews(
            int maxResults,
            NewsProviderType[] excludedTypes)
        {
            IPagedNewsProvider[] providers = GetCorrectNewsProviders(
                excludedTypes);

            return await GetNews(maxResults, providers);
        }

        private IPagedNewsProvider[] GetCorrectNewsProviders(
            NewsProviderType[] excludedTypes)
        {
            bool IsExcluded(KeyValuePair<IPagedNewsProvider, NewsProviderType> kv) 
                => !excludedTypes.Contains(kv.Value);
            
            if (excludedTypes.Any())
            {
                return _pagedNewsProviders
                    .ToDictionary(
                        provider => provider,
                        provider => provider.GetProviderType()) // KeyValuePair<IPagedNewsProvider, NewsProviderType?>
                    .Where(kv => kv.Value != null) // Filter out the nullable instances
                    .ToDictionary(
                        kv => kv.Key,
                        kv => (NewsProviderType) kv.Value) // Will not throw an exception, since the nullable instances have been filtered out
                    .Where(IsExcluded)
                    .Select(kv => kv.Key)
                    .ToArray();
            }
            return _pagedNewsProviders;
        }

        private static async Task<IEnumerable<NewsItem>> GetNews(
            int maxResults,
            IReadOnlyCollection<IPagedNewsProvider> newsProviders)
        {
            int providersCount = newsProviders.Count;
            int itemsPerProvider = maxResults / providersCount;
            int stub = maxResults % providersCount;

            List<NewsItem> items = new List<NewsItem>();

            int index = 0;
            foreach (IPagedNewsProvider provider in newsProviders)
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