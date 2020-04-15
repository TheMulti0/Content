using Content.Api;
using Content.Models;
using Kan.News;
using Mako.Reporters;

namespace Content.Services
{
    public static class NewsProviderMatcher
    {
        public static NewsProviderType? GetProviderType(this IPagedNewsProvider provider)
        {
            switch (provider)
            {
                case ReportersNewsProvider _:
                    return NewsProviderType.MakoReporters;
                
                case KanNewsProvider _:
                    return NewsProviderType.KanNews;
                
                default:
                    return null;
            }
        }
    }
}