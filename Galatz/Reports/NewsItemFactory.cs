using Content.Api;
using TwitterScraper;

namespace Galatz.Reports
{
    public static class NewsItemFactory
    {
        public static INewsItem Create(Tweet tweet)
        {
            return new NewsItem(
                NewsSource.GalatzReports,
                tweet.Text,
                null,
                AuthorFactory.Create(tweet.Author),
                tweet.PublishDate,
                tweet.Url,
                null,
                null);
        }
    }
}