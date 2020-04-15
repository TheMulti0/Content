using Content.Api;
using Content.Models;
using Kan.News;
using Mako.N12Reports;
using Mako.News;

namespace Content.Services
{
    public static class NewsProviderMatcher
    {
        public static NewsProviderType? GetProviderType(this ILatestNewsProvider provider)
        {
            switch (provider)
            {
                case MakoProvider _:
                    return NewsProviderType.Mako;
                
                default:
                    return null;
            }
        }
        public static NewsProviderType? GetProviderType(this IPagedNewsProvider provider)
        {
            switch (provider)
            {
                case N12ReportsProvider _:
                    return NewsProviderType.MakoReporters;
                
                case KanNewsProvider _:
                    return NewsProviderType.KanNews;
                
                default:
                    return null;
            }
        }
    }
}