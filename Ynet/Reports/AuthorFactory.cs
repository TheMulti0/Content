using Content.Api;

namespace Ynet.Reports
{
    public static class AuthorFactory
    {
        public static Author Create()
        {
            return new Author(
                "מבזקי ynet",
                "https://www.ynet.co.il/images/CENTRAL_1024_ynet_logo.png");
        } 
    }
}