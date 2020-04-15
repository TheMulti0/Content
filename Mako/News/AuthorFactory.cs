using Content.Api;
using Mako.News.Entities;

namespace Mako.News
{
    public static class AuthorFactory
    {
        public static Author Create(MakoRssItem item)
        {
            return new Author(
                "החדשות 12",
                null);
        } 
    }
}