using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mako.N12Reports;
using Xunit;

namespace Mako.Tests.N12Reports
{
    public class GetNewsTests
    {
        [Fact]
        public async Task Test()
        {
            await _Test(10);
        }

        [Fact]
        public async Task TestZeroPages()
        {
            await _Test(0);
        }

        // [Fact]
                    // public async Task TestMultiplePages()
                    // {
                    //     await _Test(301); // May fail when executing concurrently
                    // }

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
                        var provider = new N12ReportsProvider();
                        var items = provider
                            .GetNews(provider.MaximumItemsPerPage, cancellationToken: cts.Token)
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
        
        private async Task _Test(int maxResults)
        {
            var items = (await new N12ReportsProvider().GetNews(maxResults)).ToList();

            Assert.True(items.Count <= maxResults);
        }
    }
}