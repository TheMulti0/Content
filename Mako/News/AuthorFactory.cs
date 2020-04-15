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
                "https://rcs.mako.co.il/images/svg/news/logo-n-12.svg");
        } 
    }
}