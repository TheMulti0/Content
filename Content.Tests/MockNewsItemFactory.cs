using System;
using Content.Api;

namespace Content.Tests
{
    internal static class MockNewsItemFactory
    {
        public static NewsItem Create()
        {
            return new NewsItem(
                NewsProviderType.Mako, // Source is only relevant for the client 
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