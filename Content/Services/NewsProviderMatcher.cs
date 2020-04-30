using Calcalist.News;
using Calcalist.Reports;
using Content.Api;
using Haaretz.News;
using Kan.News;
using Mako.N12Reports;
using Mako.News;
using N0404.News;
using TheMarker.News;
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
                
                case HaaretzProvider _:
                    return NewsSource.Haaretz;

                case TheMarkerProvider _:
                    return NewsSource.TheMarker;
                
                case N0404Provider _:
                    return NewsSource.N0404;
                
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