using Content.Api;

namespace N0404.News
{
    public static class AuthorFactory
    {
        public static Author Create()
        {
            return new Author(
                "0404",
                "https://upload.wikimedia.org/wikipedia/he/thumb/9/98/0404_News_logo.svg/1200px-0404_News_logo.svg.png");
        } 
    }
}