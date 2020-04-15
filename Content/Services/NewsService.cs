using System;
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
            ILatestNewsProvider[] latestProviders = GetCorrectProviders(
                 _latestNewsProviders,
                 excludedTypes,
                 provider => provider.GetProviderType());
            
            IPagedNewsProvider[] pagedProviders = GetCorrectProviders(
                _pagedNewsProviders,
                 excludedTypes,
                 provider => provider.GetProviderType());

            return await GetNews(maxResults, latestProviders, pagedProviders);
        }
        
        private static T[] GetCorrectProviders<T>(
            T[] providers,
            NewsProviderType[] excludedTypes,
            Func<T, NewsProviderType?> getProviderType)
        {
            bool IsExcluded(KeyValuePair<T, NewsProviderType> kv) 
                => !excludedTypes.Contains(kv.Value);
            
            if (excludedTypes.Any())
            {
                return providers
                    .ToDictionary(
                        provider => provider,
                        getProviderType) // KeyValuePair<T, NewsProviderType?>
                    .Where(kv => kv.Value != null) // Filter out the nullable instances
                    .ToDictionary(
                        kv => kv.Key,
                        kv => (NewsProviderType) kv.Value) // Will not throw an exception, since the nullable instances have been filtered out
                    .Where(IsExcluded)
                    .Select(kv => kv.Key)
                    .ToArray();
            }
            return providers;
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
                int requestedItemsCount = itemsPerProvider;
                if (index + 1 == providersCount) // This is the last iteration
                {
                    requestedItemsCount += stub;
                }

                IEnumerable<NewsItem> newsItems = await provider.GetNews();
                items.AddRange(newsItems.Take(requestedItemsCount));

                index++;
            }
            
            items.AddRange(await GetPagedNews(maxResults - items.Count, pagedProviders));

            return items.OrderByDescending(item => item.Date); // Order from latest to earliest
        }

        private static async Task<List<NewsItem>> GetPagedNews(
            int maxResults,
            IReadOnlyCollection<IPagedNewsProvider> pagedProviders)
        {
            int providersCount = pagedProviders.Count;
            int itemsPerProvider = maxResults / providersCount;
            int stub = maxResults % providersCount;

            List<NewsItem> items = new List<NewsItem>();

            int index = 0;
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
            return items;
        }
    }
}