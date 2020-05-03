using System;
using Content.Api;

namespace Content.Models
{
    internal class InMemoryKey : IEquatable<InMemoryKey>
    {
        public string Title { get; set; }
        
        public NewsSource Source { get; }
        
        public DateTime Date { get; }

        public InMemoryKey(NewsItem item)
        {
            Title = item.Title;
            Source = item.Source;
            Date = item.Date;
        }

        public bool Equals(InMemoryKey other)
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
            return Equals((InMemoryKey) obj);
        }

        public override int GetHashCode() => HashCode.Combine(Title, (int) Source, Date);

        public static bool operator ==(InMemoryKey left, InMemoryKey right) => Equals(left, right);

        public static bool operator !=(InMemoryKey left, InMemoryKey right) => !Equals(left, right);
    }
}