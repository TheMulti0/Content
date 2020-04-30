using System;
using Content.Api;
using Haaretz.Entities;

namespace Haaretz.News
{
    public static class NewsItemFactory
    {
        public static NewsItem Create(HaaretzRssItem rssItem)
        {
            // Date example: Mon, 24 Feb 2020 09:44:00 +0200
            DateTime date = DateTime.Parse(rssItem.PublishDate);

            return new NewsItem(
                NewsSource.Walla,
                rssItem.Title,
                rssItem.Description,
                AuthorFactory.Create(),
                date,
                rssItem.Link,
                rssItem.Enclosure.Url,
                null);
        }
    }
}