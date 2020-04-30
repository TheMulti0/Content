using Extensions;
using Content.Api;
using Haaretz.Entities;

namespace Haaretz.News
{
    public static class NewsItemFactory
    {
        public static NewsItem Create(HaaretzRssItem rssItem)
        {
            return new NewsItem(
                NewsSource.Haaretz,
                rssItem.Title,
                rssItem.Description,
                AuthorFactory.Create(),
                rssItem.PublishDate.ToDateTime(),
                rssItem.Link,
                rssItem.Enclosure.Url,
                null);
        }
    }
}