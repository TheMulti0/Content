using Content.Api;

namespace Haaretz.News
{
    public static class AuthorFactory
    {
        public static Author Create()
        {
            return new Author(
                "הארץ",
                "https://lh3.googleusercontent.com/-dixTUU1NvVs/AAAAAAAAAAI/AAAAAAADqH4/Hs30MRP83Oo/s640/photo.jpg");
        } 
    }
}