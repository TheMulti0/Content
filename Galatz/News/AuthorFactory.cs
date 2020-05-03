using System.Linq;
using Content.Api;
using Galatz.Entities;

namespace Galatz.News
{
    public static class AuthorFactory
    {
        public static Author Create(Hashtag hashtag, Seo seo)
        {
            Talent firstTalent = hashtag.Talents.FirstOrDefault();
            
            string name = firstTalent?.Name ?? "גל\"צ";
            string imageUrl = firstTalent?.Img ?? seo.Image;
            
            return new Author(
                name,
                GalatzConstants.ToAbsoluteUrl(imageUrl));
        }
    }
}