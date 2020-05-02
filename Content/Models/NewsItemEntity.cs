using Content.Api;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Content.Models
{
    public class NewsItemEntity : NewsItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public NewsItemEntity(NewsItem item) : base(
            item.Source,
            item.Title,
            item.Description,
            item.Author,
            item.Date,
            item.Url,
            item.ImageUrl,
            item.VideoUrl)
        {
        }
    }
}