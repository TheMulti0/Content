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
        private readonly INewsDatabase _database;

        public NewsController(
            NewsService service, // So that the service will be created at some point
            INewsDatabase database)
        {
            _database = database;
        }

        [HttpGet]
        public async Task<IEnumerable<INewsItem>> Get(
            [FromQuery] int maxResults,
            [FromQuery] string excludedSources)
        {
            var excludedSourcesArray = DeserializeExcludedSources(excludedSources);

            return await _database.GetAsync(maxResults, excludedSourcesArray);
        }

        private static NewsSource[] DeserializeExcludedSources(string excludedSources)
        {
            return JsonSerializer.Deserialize<NewsSource[]>(
                excludedSources,
                new JsonSerializerOptions
                {
                    Converters =
                    {
                        new JsonStringEnumConverter()
                    }
                });
        }
    }
}
