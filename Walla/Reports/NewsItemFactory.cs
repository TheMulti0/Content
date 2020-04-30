﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using Content.Api;
using Walla.Entities;

namespace Walla.Reports
{
    public static class NewsItemFactory
    {
        private const string ContentPattern = "<br\\/>(.*)<\\/p";
        
        public static NewsItem Create(WallaRssItem rssItem)
        {
            // Date example: Mon, 24 Feb 2020 09:44:00 +0200
            DateTime date = DateTime.Parse(rssItem.PublishDate);

            string description = Regex.Match(rssItem.Description, ContentPattern).Groups.LastOrDefault()?.Value;
            
            return new NewsItem(
                NewsSource.WallaReports,
                rssItem.Title,
                description,
                AuthorFactory.Create(),
                date,
                rssItem.Link,
                null,
                null);
        }
    }
}