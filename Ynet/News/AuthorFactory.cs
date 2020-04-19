using Content.Api;

namespace Ynet.News
{
    public static class AuthorFactory
    {
        public static Author Create()
        {
            return new Author(
                "ynet",
                "https://www.ynet.co.il/images/CENTRAL_1024_ynet_logo.png");
        } 
    }
}