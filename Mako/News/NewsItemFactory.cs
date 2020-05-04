using System;
using System.Globalization;
using Content.Api;
using Mako.News.Entities;

namespace Mako.News
{
    public static class NewsItemFactory
    {
        public static INewsItem Create(MakoRssItem rssItem)
        {
            // Date example: Mon, 24 Feb 2020 09:44:00 +0200

            DateTime date = DateTime.ParseExact(
                rssItem.PublishDate,
                "ddd, d MMM yyyy HH:mm:ss K",
                CultureInfo.InvariantCulture);
            
            return new NewsItem(
                NewsSource.Mako,
                rssItem.Title,
                rssItem.ShortDescription,
                AuthorFactory.Create(),
                date,
                rssItem.Link,
                rssItem.HighResImage,
                null);
        }
    }
}