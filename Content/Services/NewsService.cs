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

            return await GetNews(
                maxResults,
                latestProviders,
                pagedProviders);
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

        private static async Task<IEnumerable<NewsItem>> GetNews(
            int maxResults,
            IReadOnlyCollection<ILatestNewsProvider> latestProviders,
            IReadOnlyCollection<IPagedNewsProvider> pagedProviders)
        {
            int latestProvidersCount = latestProviders.Count;
            int pagedProvidersCount = pagedProviders.Count;

            int providersCount = latestProvidersCount + pagedProvidersCount;
            if (providersCount == 0)
            {
                return new List<NewsItem>();
            }
            int perProvider = maxResults / providersCount;
            int stubForAll = maxResults % providersCount;
            int resultsPerLatestProvider = perProvider + stubForAll / providersCount; // TODO Make latestproviders handle stub too

            List<NewsItem> latestNewsList;
            if (latestProvidersCount > 0)
            {
                IEnumerable<NewsItem> latestNews = await GetLatestNews(resultsPerLatestProvider, latestProviders);
                latestNewsList = latestNews.ToList();
            }
            else
            {
                latestNewsList = new List<NewsItem>();
            }


            IEnumerable<NewsItem> pagedNews;
            if (pagedProvidersCount > 0)
            {
                pagedNews = await GetPagedNews(
                    maxResults,
                    resultsPerLatestProvider,
                    latestNewsList.Count,
                    pagedProviders);
            }
            else
            {
                pagedNews = new List<NewsItem>();
            }

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