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
    public class ReportersNewsProvider : IPagedNewsProvider
    {
        private const string BaseAddress = "https://www.mako.co.il";

        private readonly HttpClient _client;

        public ReportersNewsProvider(HttpClient client = null)
        {
            _client = client ?? new HttpClient();
            _client.BaseAddress = new Uri(BaseAddress);
        }

        public int MaximumItemsPerPage { get; } = 100;

        public async Task<IEnumerable<NewsItem>> GetNews(
            int maxResults,
            int firstPage = 0,
            CancellationToken cancellationToken = default)
        {
            int remainingItemsCount = maxResults;

            var totalItems = new List<NewsItem>();

            for (int pageIndex = firstPage + 1; remainingItemsCount > 0; pageIndex++)
            {
                IEnumerable<NewsItem> items = await GetItems(
                    pageIndex,
                    cancellationToken);
                List<NewsItem> itemsList = items.ToList();

                int itemsToTakeCount = GetItemsToTakeCount(
                    itemsList.Count,
                    remainingItemsCount);

                totalItems.AddRange(itemsList.Take(itemsToTakeCount));

                remainingItemsCount -= itemsToTakeCount;
                if (itemsList.Count < MaximumItemsPerPage)
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
            HttpResponseMessage response = await _client.GetAsync(
                $"/AjaxPage?jspName=getDesk12Messages.jsp&count={MaximumItemsPerPage}&page={page}",
                token);

            Stream content = await response.Content.ReadAsStreamAsync();

            IEnumerable<Report> messages = await DeserializeJson(content, token);

            return messages.Select(NewsItemFactory.Create);
        }

        public static async Task<IEnumerable<Report>> DeserializeJson(Stream content, CancellationToken token)
        {
            return await JsonSerializer.DeserializeAsync<Report[]>(
                content,
                cancellationToken: token);
        }
    }
}