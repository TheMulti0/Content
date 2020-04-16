using Content.Api;

namespace Content.Tests
{
    internal static class MockAuthorFactory
    {
        public static Author Create()
        {
            return new Author(
                "Mock author",
                null);
        }
    }
}