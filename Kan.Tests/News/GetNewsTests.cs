using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kan.News;
using Xunit;

namespace Kan.Tests.News
{
    public class GetNewsTests
    {
        [Fact]
        public async Task Test()
        {
            await _Test(9);
        }

        [Fact]
        public async Task TestZeroPages()
        {
            await _Test(0);
        }

        [Fact]
        public async Task TestMultiplePages()
        {
            await _Test(19); // May fail when executing concurrently
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
                        new KanNewsProvider(cancellationToken: cts.Token)
                            .Get()
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
            var items = (await new KanNewsProvider(maxResults).Get()).ToList();

            Assert.True(items.Count <= maxResults);
        }
    }
}