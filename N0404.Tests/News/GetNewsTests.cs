using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Content.Api;
using N0404.News;
using Xunit;

namespace N0404.Tests.News
{
    public class GetNewsTests
    {
        [Fact]
        public async Task Test()
        {
            List<NewsItem> items = (await new N0404Provider().GetNews()).ToList();

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
                        var provider = new N0404Provider();
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