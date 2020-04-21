using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Content.Api;
using Mako.N12Reports;
using Mako.N12Reports.Entities;
using Xunit;

namespace Mako.Tests.N12Reports
{
    public class ParsingTests
    {
        private const string ProjectRoot = "../../..";

        [Fact]
        public async Task TestItemsParsing()
        {
            var feed = await DeserializeJson("reporters1.json");
            IEnumerable<NewsItem> items = feed.Select(NewsItemFactory.Create);
            
            foreach (NewsItem item in items)
            {
                AssertNewsItem(item);
            }
        }

        private static Task<IEnumerable<Report>> DeserializeJson(string fileName)
        {
            return N12ReportsProvider.DeserializeJson(ReadJson(fileName), CancellationToken.None);
        }

        private static FileStream ReadJson(string fileName)
        {
            return File.OpenRead($"{ProjectRoot}/{fileName}");
        }

        private static void AssertNewsItem(NewsItem item)
        {
            // Some of these properties can never be null,
            // they are accessed in order to throw a NullReferenceException,
            // if they were not initialized

            Assert.NotNull(item);
            Assert.True(item.Title != null || item.ImageUrl != null);
            Assert.NotEqual(DateTime.MinValue, item.Date);
        }
    }
}