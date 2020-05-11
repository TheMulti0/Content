using System.Collections.Generic;
using System.Threading.Tasks;
using Content.Api;
using Galatz.Reports;
using Xunit;

namespace Galatz.Tests.Reports
{
    public class GetNewsTests
    {
        [Fact]
        public async Task Test1()
        {
            IEnumerable<INewsItem> news = await new GalatzReportsProvider().GetNews();
            
            Assert.NotEmpty(news);
        }
    }
}