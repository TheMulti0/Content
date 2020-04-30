using Content.Api;

namespace TheMarker.News
{
    public static class AuthorFactory
    {
        public static Author Create(string author)
        {
            return new Author(
                author,
                "https://www.themarker.com/tm/images/logos/tm-logo-icon.png");
        } 
    }
}