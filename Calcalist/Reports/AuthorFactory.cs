using Content.Api;

namespace Calcalist.Reports
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