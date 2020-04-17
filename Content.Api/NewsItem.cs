using System;

namespace Content.Api
{
    public class NewsItem
    {
        public NewsProviderType Source { get; }
        
        public string Title { get; }

        public string Description { get; }
        
        public Author Author { get; }

        public DateTime Date { get; }

        public string Url { get; }

        public string ImageUrl { get; }

        public string VideoUrl { get; }

        public NewsItem(
            NewsProviderType source,
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