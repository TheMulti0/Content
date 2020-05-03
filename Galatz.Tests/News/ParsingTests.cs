using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Content.Api;
using Galatz.News;
using Xunit;

namespace Galatz.Tests.News
{
    public class ParsingTests
    {
        private const string ProjectRoot = "../../..";
        
        [Fact]
        public async Task TestItemsParsing()
        {
            IEnumerable<NewsItem> items = await GalatzProvider.DeserializeItems(
                ReadJson("news1.json"),
                CancellationToken.None);
            
            foreach (NewsItem item in items)
            {
                AssertNewsItem(item);
            }
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
            Assert.NotNull(item.Title);
            Assert.NotEqual(DateTime.MinValue, item.Date);
            Assert.NotNull(item.Url);
        }
    }
}