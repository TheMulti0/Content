﻿using System;
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
                IEnumerable<NewsItemEntity> newItems = await GetNewItems();

                await _database.AddRangeAsync(newItems);

                await Task.Delay(TimeSpan.FromSeconds(_settings.NewsUpdateSecondsInterval));
            }
        }

        private async Task<IEnumerable<NewsItemEntity>> GetNewItems()
        {
            IEnumerable<INewsItem> news = await GetNews();
            IEnumerable<INewsItem> currentNews = await _database.GetAsync();

            return news
                .Where(item => !WasItemAdded(currentNews, item))
                .Select(item => new NewsItemEntity(item));
        }

        private static bool WasItemAdded(
            IEnumerable<INewsItem> currentNews,
            INewsItem item)
        {
            var itemInfo = new NewsItemInfo(item);

            return currentNews.Any(containedItem => itemInfo == new NewsItemInfo(containedItem));
        }

        public async Task<IEnumerable<INewsItem>> GetNews()
        {
            IEnumerable<INewsItem> orderedItems = OrderItems(
                await GetAllItems(_latestNewsProviders, _pagedNewsProviders));

            return orderedItems;
        }

        private static async Task<List<INewsItem>> GetAllItems(
            IEnumerable<ILatestNewsProvider> latestProviders,
            IEnumerable<IPagedNewsProvider> pagedProviders)
        {
            var items = new List<INewsItem>();

            foreach (ILatestNewsProvider provider in latestProviders)
            {
                try
                {
                    items.AddRange(await provider.GetNews());
                }
                catch
                {
                    // ignored
                }
            }
            foreach (IPagedNewsProvider provider in pagedProviders)
            {
                try
                {
                    items.AddRange(
                        await provider.GetNews(provider.MaximumItemsPerPage));
                }
                catch
                {
                    // ignored
                }
            }
            return items;
        }

        private static IEnumerable<INewsItem> OrderItems(IEnumerable<INewsItem> items)
        {
            return items
                .OrderByDescending(item => item.Date);
        }
    }
}