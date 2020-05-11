using Content.Api;
using TwitterScraper;

namespace Kan.Reports
{
    public static class NewsItemFactory
    {
        public static INewsItem Create(Tweet tweet)
        {
            return new NewsItem(
                NewsSource.KanReports,
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