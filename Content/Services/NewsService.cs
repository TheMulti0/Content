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

            return await GetNews(
                maxResults,
                latestProviders,
                pagedProviders);
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
            int pagedProvidersCount = pagedProviders.Count;
            
            int providersCount = latestProviders.Count + pagedProvidersCount;
            int resultsPerLatestProvider = maxResults / providersCount; // TODO Make latestproviders handle stub too

            IEnumerable<NewsItem> latestNews = await GetLatestNews(resultsPerLatestProvider, latestProviders);

            List<NewsItem> latestNewsList = latestNews.ToList();
            
            IEnumerable<NewsItem> pagedNews = await GetPagedNews(
                maxResults,
                resultsPerLatestProvider,
                latestNewsList.Count,
                pagedProviders);

            return latestNewsList
                .Concat(pagedNews)
                .OrderByDescending(item => item.Date);
        }

        private static ValueTask<IEnumerable<NewsItem>> GetLatestNews(
            int resultsPerLatestProvider,
            IEnumerable<ILatestNewsProvider> latestProviders)
        {
            async ValueTask<IEnumerable<NewsItem>> GetNews(ILatestNewsProvider provider, int _)
            {
                IEnumerable<NewsItem> items = await provider.GetNews();

                return items.Take(resultsPerLatestProvider);
            }

            return GetProviderNews(latestProviders, GetNews);
        }

        private static ValueTask<IEnumerable<NewsItem>> GetPagedNews(
            int maxResults,
            int resultsPerLatestProvider,
            int receivedItemsCount,
            IReadOnlyCollection<IPagedNewsProvider> pagedProviders)
        {
            int overallGap = maxResults - receivedItemsCount - resultsPerLatestProvider;
            
            int pagedProvidersCount = pagedProviders.Count;

            int extra = overallGap / pagedProvidersCount;
            int resultsPerPagedProvider = resultsPerLatestProvider + extra;
            int stub = overallGap % pagedProvidersCount;

            async ValueTask<IEnumerable<NewsItem>> GetNews(
                IPagedNewsProvider provider,
                int index)
            {
                int resultsToTake = resultsPerPagedProvider;
                if (index == pagedProvidersCount)
                {
                    resultsToTake += stub;
                }
                
                return await provider.GetNews(resultsToTake);
            }

            return GetProviderNews(pagedProviders, GetNews);
        }

        private static ValueTask<IEnumerable<NewsItem>> GetProviderNews<T>(
            IEnumerable<T> providers,
            Func<T, int, ValueTask<IEnumerable<NewsItem>>> getNews)
        {
            return providers
                .ToAsyncEnumerable()
                .SelectAwait(getNews)
                .AggregateAsync((lhs, rhs) => lhs.Concat(rhs));
        }
    }
}