using Content.Api;

namespace TheMarker.News
{
    public static class AuthorFactory
    {
        public static Author Create(string author)
        {
            return new Author(
                author,
                "https://www.TheMarker.co.il/favicon64x64.ico");
        } 
    }
}