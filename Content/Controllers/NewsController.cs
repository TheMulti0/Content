using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Content.Api;
using Content.Models;
using Content.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Content.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly NewsService _news;
        private readonly INewsDatabase _database;

        public NewsController(NewsService news, INewsDatabase database)
        {
            _news = news;
            _database = database;
            IEnumerable<NewsItem> newsItems = news.GetNews(
                10,
                new NewsSource[0]).Result;

            IEnumerable<NewsItemEntity> entities = newsItems.Select(item => new NewsItemEntity(item));
            
            database.AddRangeAsync(entities).Wait();

            database.RemoveAsync(entities.First());
        }

        [HttpGet]
        public async Task<IEnumerable<NewsItemEntity>> Get(
            [FromQuery] int maxResults,
            [FromQuery] string excludedSources)
        {
            return await _database.GetAsync();
            // NewsSource[] excludedSourcesArray = JsonSerializer.Deserialize<NewsSource[]>(
            //     excludedSources,
            //     new JsonSerializerOptions
            //     {
            //         Converters = { new JsonStringEnumConverter() }
            //     });
            //
            // return _news.GetNews(
            //     maxResults,
            //     excludedSourcesArray);
        }
    }
}
