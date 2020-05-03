﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using Content.Api;
using Galatz.Entities;

namespace Galatz.News
{
    public static class NewsItemFactory
    {
        private const string TimePattern = "[0-9]{4}";
        
        public static NewsItem Create(Hashtag hashtag, Seo seo)
        {
            var fullDate = DateTime.Parse(GetFullDate(hashtag));

            return new NewsItem(
                NewsSource.Galatz,
                hashtag._Hashtag,
                null,
                AuthorFactory.Create(hashtag, seo),
                fullDate,
                GalatzConstants.ToAbsoluteUrl(hashtag.Url),
                GalatzConstants.ToAbsoluteUrl(hashtag.Img),
                null);
        }

        private static string GetFullDate(Hashtag hashtag)
        {
            string time = Regex.Match(hashtag.Url, TimePattern, RegexOptions.RightToLeft).Groups.FirstOrDefault()?.Value;
            
            string formattedTime = time?.Insert(2, ":"); // Add hour:second seperator

            return formattedTime == null 
                ? hashtag.Date 
                : $"{hashtag.Date} {formattedTime}";
        }
    }
}