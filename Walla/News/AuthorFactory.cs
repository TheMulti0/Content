using Content.Api;

namespace Walla.News
{
    public static class AuthorFactory
    {
        public static Author Create()
        {
            return new Author(
                "וואלה! חדשות",
                "https://news.walla.co.il/images/logo-mobile.svg");
        } 
    }
}