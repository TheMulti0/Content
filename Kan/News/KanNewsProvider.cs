using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Content.Api;
using Extensions;
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
        
        public Task<IEnumerable<INewsItem>> GetNews(
            int maxResults,
            int firstPage = 0,
            CancellationToken cancellationToken = default)
        {
            return PagedNewsProvider.GetNews(
                maxResults,
                MaximumItemsPerPage,
                GetItems,
                firstPage,
                cancellationToken);
        }
        
        private async Task<IEnumerable<INewsItem>> GetItems(int page, CancellationToken token)
        {
            HttpResponseMessage response = await _client.GetAsync($"/program/getMoreProgramNews.aspx?count={page}", token);
            string html = await response.Content.ReadAsStringAsync();

            return ParseItems(html);
        }

        public static IEnumerable<INewsItem> ParseItems(string html)
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