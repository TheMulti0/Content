using Content.Api;

namespace N0404.News
{
    public static class AuthorFactory
    {
        public static Author Create(string author)
        {
            return new Author(
                author,
                "https://www.calcalist.co.il/favicon64x64.ico");
        } 
    }
}