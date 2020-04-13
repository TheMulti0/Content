using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Content.Api;
using Mako.Reporters.Entities;

namespace Mako.Reporters
{
    public class ReportersNewsProvider : INewsProvider
    {
        private const string BaseAddress = "https://www.mako.co.il";
        private const int MaxItemsPerPage = 100;

        private readonly int _maxResults;
        private readonly int _firstPage;
        private readonly CancellationToken _cancellationToken;
        private readonly HttpClient _client;

        public ReportersNewsProvider(
            int maxResults = MaxItemsPerPage,
            int firstPage = 0, // Zero based
            CancellationToken cancellationToken = default,
            HttpClient client = null)
        {
            _maxResults = maxResults; // TODO Handle zero
            _firstPage = firstPage; // TODO Handle negative
            _cancellationToken = cancellationToken;
            
            _client = client ?? new HttpClient();
            _client.BaseAddress = new Uri(BaseAddress);
        }

        public async Task<IEnumerable<NewsItem>> Get()
        {
            int remainingItemsCount = _maxResults;

            var totalItems = new List<NewsItem>();

            int firstPage = _firstPage + 1;

            for (int pageIndex = firstPage; remainingItemsCount > 0; pageIndex++)
            {
                IEnumerable<NewsItem> items = await GetItems(
                    pageIndex,
                    _cancellationToken);
                var itemsList = items.ToList();

                int itemsToTakeCount = GetItemsToTakeCount(
                    itemsList.Count,
                    remainingItemsCount);

                totalItems.AddRange(itemsList.Take(itemsToTakeCount));

                remainingItemsCount -= itemsToTakeCount;
                if (itemsList.Count < MaxItemsPerPage) 
                {
                    // Meaning there are less than maximum in this page, which means this is for sure the last page
                    // (although the last page can contain 100 elements, in that case the loop will operate again and receive zero items
                    break;
                }
            }

            return totalItems;
        }

        private static int GetItemsToTakeCount(int receivedItemsCount, int remainingItemsCount)
        {
            return Math.Min(
                receivedItemsCount,
                remainingItemsCount);
        }

        private async Task<IEnumerable<NewsItem>> GetItems(
            int page,
            CancellationToken token)
        {
            var response = await _client.GetAsync(
                $"/AjaxPage?jspName=getDesk12Messages.jsp&count={MaxItemsPerPage}&page={page}",
                token);

            var content = await response.Content.ReadAsStreamAsync();

            var messages = await DeserializeJson(content, token);

            return messages.Select(NewsItemFactory.FromReport);
        }

        public static async Task<IEnumerable<Report>> DeserializeJson(Stream content, CancellationToken token)
        {
            return await JsonSerializer.DeserializeAsync<Report[]>(
                content,
                cancellationToken: token);
        }
    }
}