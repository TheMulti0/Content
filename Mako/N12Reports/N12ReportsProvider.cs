using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Content.Api;
using Extensions;
using Mako.N12Reports.Entities;

namespace Mako.N12Reports
{
    public class N12ReportsProvider : IPagedNewsProvider
    {
        private const string BaseAddress = "https://www.mako.co.il";

        private readonly HttpClient _client;

        public N12ReportsProvider(HttpClient client = null)
        {
            _client = client ?? new HttpClient();
            _client.BaseAddress = new Uri(BaseAddress);
        }

        public int MaximumItemsPerPage { get; } = 100;

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

        private async Task<IEnumerable<INewsItem>> GetItems(
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