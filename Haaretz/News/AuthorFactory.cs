using Content.Api;

namespace Haaretz.News
{
    public static class AuthorFactory
    {
        public static Author Create()
        {
            return new Author(
                "הארץ",
                "https://svgshare.com/i/Kcj.svg");
        } 
    }
}