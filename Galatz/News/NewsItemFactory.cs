using System;
using Content.Api;
using Galatz.Entities;

namespace Galatz.News
{
    public static class NewsItemFactory
    {
        public static NewsItem Create(Hashtag hashtag, Seo seo)
        {
            return new NewsItem(
                NewsSource.Calcalist,
                hashtag._Hashtag,
                null,
                AuthorFactory.Create(hashtag, seo),
                DateTime.Parse(hashtag.Date),
                GalatzConstants.ToAbsoluteUrl(hashtag.Url),
                GalatzConstants.ToAbsoluteUrl(hashtag.Img),
                null);
        }
    }
}