using System.Collections.Generic;
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
        public Task<IEnumerable<NewsItem>> Get()
        {
            return _news.GetNews(30);
        }
    }
}
