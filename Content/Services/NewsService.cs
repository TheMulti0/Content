using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Content.Api;
using Content.Models;

namespace Content.Services
{
    public class NewsService
    {
        private readonly ILatestNewsProvider[] _latestNewsProviders;
        private readonly IPagedNewsProvider[] _pagedNewsProviders;

        public NewsService(
            IEnumerable<ILatestNewsProvider> latestNewsProviders,
            IEnumerable<IPagedNewsProvider> pagedNewsProviders)
        {
            _latestNewsProviders = latestNewsProviders.ToArray();
            _pagedNewsProviders = pagedNewsProviders.ToArray();
        }

        public async Task<IEnumerable<NewsItem>> GetNews(
            int maxResults,
            NewsProviderType[] excludedTypes)
        {
            ILatestNewsProvider[] latestProviders = GetCorrectLatestProviders(excludedTypes);
            IPagedNewsProvider[] pagedProviders = GetCorrectPagedProviders(excludedTypes);

            return await GetNews(maxResults, latestProviders, pagedProviders);
        }

        private ILatestNewsProvider[] GetCorrectLatestProviders(
            NewsProviderType[] excludedTypes)
        {
            bool IsExcluded(KeyValuePair<ILatestNewsProvider, NewsProviderType> kv) 
                => !excludedTypes.Contains(kv.Value);
            
            if (excludedTypes.Any())
            {
                return _latestNewsProviders
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
            return _latestNewsProviders;
        }
        
        private IPagedNewsProvider[] GetCorrectPagedProviders(
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
            IReadOnlyCollection<ILatestNewsProvider> latestProviders,
            IReadOnlyCollection<IPagedNewsProvider> pagedProviders)
        {
            int providersCount = latestProviders.Count + pagedProviders.Count;
            int itemsPerProvider = maxResults / providersCount;
            int stub = maxResults % providersCount;

            List<NewsItem> items = new List<NewsItem>();

            int index = 0;
            foreach (ILatestNewsProvider provider in latestProviders)
            {
                IEnumerable<NewsItem> result = await provider.GetNews();
                items.AddRange(result.Take(itemsPerProvider));
                
                index++;
            }
            
            foreach (IPagedNewsProvider provider in pagedProviders)
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