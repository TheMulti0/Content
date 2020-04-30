using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Content.Api;

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
            NewsSource[] excludedSources)
        {
            ILatestNewsProvider[] latestProviders = GetCorrectProviders(
                 _latestNewsProviders,
                 excludedSources,
                 provider => provider.GetProviderType());
            
            IPagedNewsProvider[] pagedProviders = GetCorrectProviders(
                _pagedNewsProviders,
                 excludedSources,
                 provider => provider.GetProviderType());


            return await GetAllItems(maxResults, latestProviders, pagedProviders);
        }

        private static async Task<IEnumerable<NewsItem>> GetAllItems(
            int maxResults,
            IEnumerable<ILatestNewsProvider> latestProviders,
            IEnumerable<IPagedNewsProvider> pagedProviders)
        {
            IEnumerable<NewsItem> orderedItems = OrderItems(
                await GetAllItems(latestProviders, pagedProviders));

            return maxResults < 1
                ? orderedItems
                : orderedItems.Take(maxResults);
        }

        private static async Task<List<NewsItem>> GetAllItems(
            IEnumerable<ILatestNewsProvider> latestProviders,
            IEnumerable<IPagedNewsProvider> pagedProviders)
        {
            var items = new List<NewsItem>();

            foreach (ILatestNewsProvider provider in latestProviders)
            {
                items.AddRange(await provider.GetNews());
            }
            foreach (IPagedNewsProvider provider in pagedProviders)
            {
                items.AddRange(
                    await provider.GetNews(provider.MaximumItemsPerPage));
            }
            return items;
        }

        private static IEnumerable<NewsItem> OrderItems(List<NewsItem> items)
        {
            return items
                .OrderByDescending(item => item.Date);
        }

        private static T[] GetCorrectProviders<T>(
            T[] providers,
            NewsSource[] excludedSources,
            Func<T, NewsSource?> getProviderType)
        {
            bool IsExcluded(KeyValuePair<T, NewsSource> kv) 
                => !excludedSources.Contains(kv.Value);
            
            if (excludedSources.Any())
            {
                return providers
                    .ToDictionary(
                        provider => provider,
                        getProviderType) // KeyValuePair<T, NewsProviderType?>
                    .Where(kv => kv.Value != null) // Filter out the nullable instances
                    .ToDictionary(
                        kv => kv.Key,
                        kv => (NewsSource) kv.Value) // Will not throw an exception, since the nullable instances have been filtered out
                    .Where(IsExcluded)
                    .Select(kv => kv.Key)
                    .ToArray();
            }
            return providers;
        }
    }
}