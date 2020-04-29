using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Calcalist.News;
using Calcalist.Reports;
using Content.Api;
using Xunit;

namespace Calcalist.Tests.Reports
{
    public class GetNewsTests
    {
        [Fact]
        public async Task Test()
        {
            List<NewsItem> items = (await new CalcalistReportsProvider().GetNews()).ToList();

            Assert.True(items.Any());
        }

        [Fact]
        public void TestWithCancellation()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromMilliseconds(25));

            Assert.ThrowsAny<OperationCanceledException>(
                () =>
                {
                    try
                    {
                        var provider = new CalcalistReportsProvider();
                        var items = provider
                            .GetNews(cts.Token)
                            .Result
                            .ToList();
                    }
                    catch (AggregateException e)
                    {
                        List<OperationCanceledException> canceledExceptions = e.InnerExceptions
                            .Cast<OperationCanceledException>()
                            .ToList();
                        if (canceledExceptions.Any())
                        {
                            throw canceledExceptions.First();
                        }
                    }
                });
        }
    }
}