using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Content.Api;
using Content.Services;
using Microsoft.AspNetCore.Mvc;

namespace Content.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly NewsService _news;

        public NewsController(NewsService news)
        {
            _news = news;
        }

        [HttpGet]
        public Task<IEnumerable<NewsItem>> Get(
            [FromQuery] int maxResults,
            [FromQuery] string excludedSources)
        {
            NewsSource[] excludedSourcesArray = JsonSerializer.Deserialize<NewsSource[]>(
                excludedSources,
                new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter() }
                });
            return _news.GetNews(
                maxResults,
                excludedSourcesArray);
        }
    }
}
