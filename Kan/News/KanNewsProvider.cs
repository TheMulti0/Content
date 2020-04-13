using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Content.Api;
using HtmlAgilityPack;

namespace Kan.News
{
    public class KanNewsProvider : INewsProvider
    {
        internal const string BaseAddress = "https://www.kan.org.il";
        private const int MaxItemsPerPage = 9;

        private readonly int _maxResults;
        private readonly int _firstPage;
        private readonly CancellationToken _cancellationToken;
        private readonly HttpClient _client;

        public KanNewsProvider(
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
            _client.DefaultRequestHeaders.Add("User-Agent","Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.181 Safari/537.36");
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
                .Select(KanNewsItemFactory.Create);
        }
        
    }
}