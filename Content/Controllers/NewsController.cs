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
        }

        [HttpGet]
        public async Task<IEnumerable<NewsItemEntity>> Get(
            [FromQuery] int maxResults,
            [FromQuery] string excludedSources)
        {
            return await _database.GetAsync();
        }
    }
}
