using Content.Api;
using HtmlAgilityPack;

namespace Kan.News
{
    public static class AuthorFactory
    {
        public static Author FromAuthor(HtmlNode additionalInfo)
        {
            string name = additionalInfo
                .FirstOrDefault(node => node.HasClass("additinal_info_cat"))
                .InnerText;
            
            return new Author(
                name,
                KanConstants.IconAddress);
        }
    }
}