using Calcalist.News;
using Calcalist.Reports;
using Content.Api;
using Kan.News;
using Mako.N12Reports;
using Mako.News;
using Walla.News;
using Walla.Reports;
using Ynet.News;
using Ynet.Reports;

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
                
                case YnetProvider _:
                    return NewsSource.Ynet;
                
                case YnetReportsProvider _:
                    return NewsSource.YnetReports;

                case CalcalistProvider _:
                    return NewsSource.Calcalist;
                
                case CalcalistReportsProvider _:
                    return NewsSource.CalcalistReports;
                
                case WallaProvider _:
                    return NewsSource.Walla;
                
                case WallaReportsProvider _:
                    return NewsSource.WallaReports;
                
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
                    return NewsSource.Kan;
                
                default:
                    return null;
            }
        }
    }
}