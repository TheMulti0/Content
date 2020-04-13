using System.Collections.Generic;
using System.Threading.Tasks;
using Content.Api;
using Mako.Reporters;
using Microsoft.AspNetCore.Mvc;

namespace Content.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        [HttpGet]
        public Task<IEnumerable<NewsItem>> Get()
        {
            return new ReportersNewsProvider().Get();
        }
    }
}
