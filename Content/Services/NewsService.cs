﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Content.Api;
using Content.Models;

namespace Content.Services
{
    public class NewsService
    {
        private readonly INewsDatabase _database;
        private readonly ILatestNewsProvider[] _latestNewsProviders;
        private readonly IPagedNewsProvider[] _pagedNewsProviders;

        public NewsService(
            INewsDatabase database,
            IEnumerable<ILatestNewsProvider> latestNewsProviders,
            IEnumerable<IPagedNewsProvider> pagedNewsProviders)
        {
            _database = database;
            _latestNewsProviders = latestNewsProviders.ToArray();
            _pagedNewsProviders = pagedNewsProviders.ToArray();
            
            Update();
        }

        private void Update()
        {
            Task.Run(UpdateDatabase);
        }

        private async Task UpdateDatabase()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(5));

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

            foreach (NewsItemEntity containedItem in currentNews)
            {
                if (itemInfo != new NewsItemInfo(containedItem))
                {
                    return false;
                }
            }
            return true;
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