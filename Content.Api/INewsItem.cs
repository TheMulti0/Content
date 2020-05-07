using System;

namespace Content.Api
{
    public interface INewsItem
    {
        NewsSource Source { get; }
        
        string Title { get; }

        string Description { get; }
        
        Author Author { get; }

        DateTime Date { get; }

        string Url { get; }

        string ImageUrl { get; }

        string VideoUrl { get; }
    }
}