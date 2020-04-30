using Content.Api;

namespace Walla.Reports
{
    public static class AuthorFactory
    {
        public static Author Create()
        {
            return new Author(
                "וואלה! מבזקים",
                "https://news.walla.co.il/images/logo-mobile.svg");
        } 
    }
}