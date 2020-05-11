using Content.Api;
using TwitterScraper;

namespace Galatz.Reports
{
    public static class AuthorFactory
    {
        public static Author Create(User user)
        {
            return new Author(
                user.Name,
                "https://upload.wikimedia.org/wikipedia/he/thumb/3/30/GaltzLogo.svg/1200px-GaltzLogo.svg.png");
        }
    }
}