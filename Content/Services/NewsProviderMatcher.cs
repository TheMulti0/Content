using Content.Api;
using Kan.News;
using Mako.N12Reports;
using Mako.News;

namespace Content.Services
{
    public static class NewsProviderMatcher
    {
        public static NewsSource? GetProviderType(this ILatestNewsProvider provider)
        {
            switch (provider)
            {
                case MakoProvider _:
                    return NewsSource.Mako;
                
                default:
                    return null;
            }
        }
        public static NewsSource? GetProviderType(this IPagedNewsProvider provider)
        {
            switch (provider)
            {
                case N12ReportsProvider _:
                    return NewsSource.MakoReporters;
                
                case KanNewsProvider _:
                    return NewsSource.KanNews;
                
                default:
                    return null;
            }
        }
    }
}