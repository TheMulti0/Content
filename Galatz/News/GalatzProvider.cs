using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Content.Api;
using Galatz.Entities;

namespace Galatz.News
{
    public class GalatzProvider : ILatestNewsProvider
    {
        private readonly HttpClient _client;

        public GalatzProvider(HttpClient client = null)
        {
            _client = client ?? new HttpClient();
        }

        public async Task<IEnumerable<NewsItem>> GetNews(CancellationToken cancellationToken = default)
        {
            HttpResponseMessage response = await _client.GetAsync(
                GalatzConstants.ToAbsoluteUrl("/umbraco/api/home/GetHomePageData?rootId=1051"),
                cancellationToken);

            Stream json = await response.Content.ReadAsStreamAsync();

            return await DeserializeItems(json, cancellationToken);
        }

        public static async Task<IEnumerable<NewsItem>> DeserializeItems(
            Stream json,
            CancellationToken cancellationToken)
        {
            var data = await JsonSerializer.DeserializeAsync<HomePageData>(
                json,
                cancellationToken: cancellationToken);

            return data.Hashtags
                .Where(hashtag => !hashtag.IsNewsPage)
                .Select(hashtag => NewsItemFactory.Create(hashtag, data.Seo));
        }
    }
}