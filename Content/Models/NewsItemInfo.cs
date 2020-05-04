using System;
using Content.Api;

namespace Content.Models
{
    internal class NewsItemInfo : IEquatable<NewsItemInfo>
    {
        public string Title { get; }
        
        public NewsSource Source { get; }
        
        public DateTime Date { get; }

        public NewsItemInfo(INewsItem item)
        {
            Title = item.Title;
            Source = item.Source;
            Date = item.Date;
        }

        public bool Equals(NewsItemInfo other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Title == other.Title &&
                   Source == other.Source &&
                   Date.Equals(other.Date);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((NewsItemInfo) obj);
        }

        public override int GetHashCode() => HashCode.Combine(Title, (int) Source, Date);

        public static bool operator ==(NewsItemInfo left, NewsItemInfo right) => Equals(left, right);

        public static bool operator !=(NewsItemInfo left, NewsItemInfo right) => !Equals(left, right);
    }
}