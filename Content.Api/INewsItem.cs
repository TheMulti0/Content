using System;

namespace Content.Api
{
    public interface INewsItem
    {
        NewsSource Source { get; set; }
        
        string Title { get; set; }

        string Description { get; set; }
        
        Author Author { get; set; }

        DateTime Date { get; set; }

        string Url { get; set; }

        string ImageUrl { get; set; }

        string VideoUrl { get; set; }
    }
}