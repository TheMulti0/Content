using System;
using System.Collections.Generic;
using System.IO;
using Content.Api;
using Walla.Reports;
using Xunit;

namespace Walla.Tests.Reports
{
    public class ParsingTests
    {
        private const string ProjectRoot = "../../..";
        
        [Fact]
        public void TestItemsParsing()
        {
            IEnumerable<INewsItem> items = WallaReportsProvider.DeserializeItems(ReadXml("reports1.xml"));
            
            foreach (INewsItem item in items)
            {
                AssertNewsItem(item);
            }
        }

        private static FileStream ReadXml(string fileName)
        {
            return File.OpenRead($"{ProjectRoot}/{fileName}");
        }

        private static void AssertNewsItem(INewsItem item)
        {
            // Some of these properties can never be null,
            // they are accessed in order to throw a NullReferenceException,
            // if they were not initialized
            
            Assert.NotNull(item);
            Assert.NotNull(item.Title);
            Assert.NotNull(item.Description);
            Assert.NotEqual(DateTime.MinValue, item.Date);
            Assert.NotNull(item.Url);
        }
    }
}