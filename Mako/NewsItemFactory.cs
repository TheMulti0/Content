﻿using System;
using System.Linq;
using Content.Api;
using Mako.Reporters.Entities;

namespace Mako.Reporters
{
    public static class NewsItemFactory
    {
        private static DateTime _unixEpoch = new DateTime(1970, 1, 1);

        public static NewsItem FromReport(Report message)
        {
            return new NewsItem(
                $"Report by {message.Reporter.Nickname}",
                message.Content,
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
            
            return firstMediaInfo == null 
                ? message.Reporter.Details.Image 
                : firstMediaInfo.HighResolutionImage;
        }
    }
}