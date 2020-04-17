using System;
using System.Linq;
using Content.Api;
using Mako.N12Reports.Entities;

namespace Mako.N12Reports
{
    public static class NewsItemFactory
    {
        private static DateTime _unixEpoch = new DateTime(1970, 1, 1);

        public static NewsItem Create(Report message)
        {
            return new NewsItem(
                NewsSource.MakoReporters,
                "",
                message.Content,
                AuthorFactory.Create(message.Reporter), 
                UnixTime(message.PublishedDate),
                null,
                GetImageUrl(message),
                null);
        }

        private static DateTime UnixTime(long ticks)
        {
            return _unixEpoch.AddMilliseconds(ticks).ToLocalTime();
        }

        private static string GetImageUrl(Report message)
        {
            var firstMediaInfo = message.MediaInfos.FirstOrDefault();
            
            return firstMediaInfo?.HighResolutionImage;
        }
    }
}