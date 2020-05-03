using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Content.Api;
using Content.Models;
using Microsoft.Extensions.Options;

namespace Content.Services
{
    public class NewsService
    {
        private readonly NewsSettings _settings;
        private readonly INewsDatabase _database;
        private readonly ILatestNewsProvider[] _latestNewsProviders;
        private readonly IPagedNewsProvider[] _pagedNewsProviders;

        public NewsService(
            IOptions<NewsSettings> settings,
            INewsDatabase database,
            IEnumerable<ILatestNewsProvider> latestNewsProviders,
            IEnumerable<IPagedNewsProvider> pagedNewsProviders)
        {
            _settings = settings.Value;
            _database = database;
            _latestNewsProviders = latestNewsProviders.ToArray();
            _pagedNewsProviders = pagedNewsProviders.ToArray();
            
            StartUpdating(CancellationToken.None);
        }

        public void StartUpdating(CancellationToken token)
        {
            Task.Run(UpdateDatabase, token);
        }

        private async Task UpdateDatabase()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(_settings.NewsUpdateSecondsInterval));

                IEnumerable<NewsItemEntity> newItems = await GetNewItems();

                await _database.AddRangeAsync(newItems);
            }
        }

        private async Task<IEnumerable<NewsItemEntity>> GetNewItems()
        {
            IEnumerable<NewsItem> news = await GetNews();
            IEnumerable<NewsItemEntity> currentNews = await _database.GetAsync();

            return news
                .Where(item => !WasItemAdded(currentNews, item))
                .Select(item => new NewsItemEntity(item));
        }

        private static bool WasItemAdded(
            IEnumerable<NewsItemEntity> currentNews,
            NewsItem item)
        {
            var itemInfo = new NewsItemInfo(item);

            return currentNews.Any(containedItem => itemInfo == new NewsItemInfo(containedItem));
        }

        public async Task<IEnumerable<NewsItem>> GetNews()
        {
            IEnumerable<NewsItem> orderedItems = OrderItems(
                await GetAllItems(_latestNewsProviders, _pagedNewsProviders));

            return orderedItems;
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

        private static IEnumerable<NewsItem> OrderItems(IEnumerable<NewsItem> items)
        {
            return items
                .OrderByDescending(item => item.Date);
        }
    }
}