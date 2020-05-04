using System;
using Content.Api;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Content.Models
{
    public class NewsItemEntity : INewsItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public NewsSource Source { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public Author Author { get; set; }
        
        public DateTime Date { get; set; }
        
        public string Url { get; set; }
        
        public string ImageUrl { get; set; }
        
        public string VideoUrl { get; set; }

        public NewsItemEntity(INewsItem item)
        {
            Source = item.Source;
            Title = item.Title;
            Description = item.Description;
            Author = item.Author;
            Date = item.Date;
            Url = item.Url;
            ImageUrl = item.ImageUrl;
            VideoUrl = item.VideoUrl;
        }
    }
}