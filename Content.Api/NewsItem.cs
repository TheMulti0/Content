using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Content.Api
{
    public class NewsItem
    {
        public NewsSource Source { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }
        
        public Author Author { get; set; }

        public DateTime Date { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public string VideoUrl { get; set; }

        public NewsItem(
            NewsSource source,
            string title,
            string description,
            Author author,
            DateTime date,
            string url,
            string imageUrl,
            string videoUrl)
        {
            Title = title;
            Description = description;
            Author = author;
            Date = date;
            Url = url;
            ImageUrl = imageUrl;
            VideoUrl = videoUrl;
            Source = source;
        }
    }
}