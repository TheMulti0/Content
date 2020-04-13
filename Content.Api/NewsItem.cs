using System;

namespace Content.Api
{
    public class NewsItem
    {
        public string Title { get; }

        public string Description { get; }

        public DateTime Date { get; }

        public string Url { get; }

        public string ImageUrl { get; }

        public string VideoUrl { get; }

        public NewsItem(
            string title,
            string description,
            DateTime date,
            string url,
            string imageUrl,
            string videoUrl)
        {
            Title = title;
            Description = description;
            Date = date;
            Url = url;
            ImageUrl = imageUrl;
            VideoUrl = videoUrl;
        }
    }
}