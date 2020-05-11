using Content.Api;
using TwitterScraper;

namespace Kan.Reports
{
    public static class AuthorFactory
    {
        public static Author Create(User user)
        {
            return new Author(
                user.Name,
                KanConstants.IconAddress);
        }
    }
}