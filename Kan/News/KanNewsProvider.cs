using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Content.Api;
using HtmlAgilityPack;

namespace Kan.News
{
    public class KanNewsProvider : IPagedNewsProvider
    {
        internal const string BaseAddress = "https://www.kan.org.il";

        private readonly HttpClient _client;

        public KanNewsProvider(HttpClient client = null)
        {
            _client = client ?? new HttpClient();
            _client.BaseAddress = new Uri(BaseAddress);
        }

        public int MaximumItemsPerPage { get; } = 9;
        
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
                var itemsList = items.ToList();

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

        private async Task<IEnumerable<NewsItem>> GetItems(int page, CancellationToken token)
        {
            HttpResponseMessage response = await _client.GetAsync($"/program/getMoreProgramNews.aspx?count={page}", token);
            string html = await response.Content.ReadAsStringAsync();

            return ParseItems(html);
        }

        public static IEnumerable<NewsItem> ParseItems(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            return doc
                .DocumentNode
                .FirstElementOrDefault("ol")
                .ChildNodes
                .Where(item => item.Name == "li")
                .Select(NewsItemFactory.Create);
        }
    }
}