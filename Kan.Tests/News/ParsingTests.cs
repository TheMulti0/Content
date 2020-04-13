using System;
using System.Collections.Generic;
using System.IO;
using Content.Api;
using HtmlAgilityPack;
using Kan.News;
using Xunit;

namespace Kan.Tests.News
{
    public class ParsingTests
    {
        private const string ProjectRoot = "../../..";
        
        [Fact]
        public void TestItemsParsing()
        {
            string html = ReadHtmlText("newsitems.html");
            IEnumerable<NewsItem> items = KanNewsProvider.ParseItems(html);
            
            Assert.NotNull(items);
            
            foreach (NewsItem item in items)
            {
                AssertNewsItem(item);
                Assert.NotNull(item.Url);
            }
        }

        [Fact]
        public void TestItemParsing1() => TestItemParsing("newsitem1.html");
        
        [Fact]
        public void TestItemParsing2() => TestItemParsing("newsitem2.html");
        
        [Fact]
        public void TestItemParsing3() => TestItemParsing("newsitem3.html");
        
        private static NewsItem TestItemParsing(string fileName)
        {
            HtmlNode newsItemNode = ReadHtml(fileName);
            NewsItem item = NewsItemFactory.Create(newsItemNode);
            AssertNewsItem(item);

            return item;
        }

        private static HtmlNode ReadHtml(string fileName)
        {
            string readHtmlText = ReadHtmlText(fileName);
            return HtmlNode.CreateNode(readHtmlText);
        }

        private static string ReadHtmlText(string fileName)
        {
            return File.ReadAllText($"{ProjectRoot}/{fileName}");
        }

        private static void AssertNewsItem(NewsItem item)
        {
            // Some of these properties can never be null,
            // they are accessed in order to throw a NullReferenceException,
            // if they were not initialized

            Assert.NotNull(item);
            Assert.NotNull(item.Title);
            Assert.NotEqual(DateTime.MinValue, item.Date);
        }
    }
}