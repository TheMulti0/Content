using System;
using Content.Api;

namespace Content.Tests
{
    internal static class MockNewsItemFactory
    {
        public static NewsItem Create()
        {
            return new NewsItem(
                "Mock newsitem",
                "This is for tests only",
                MockAuthorFactory.Create(),
                DateTime.Now,
                null,
                null,
                null);
        }
    }
}