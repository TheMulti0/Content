using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Content.Api;

namespace Extensions
{
    public static class PagedNewsProvider
    {
        public static async Task<IEnumerable<NewsItem>> GetNews(
            int maxResults,
            int maxItemsPerPage,
            Func<int, CancellationToken, Task<IEnumerable<NewsItem>>> getItems,
            int firstPage,
            CancellationToken cancellationToken )
        {
            int remainingItemsCount = maxResults;

            var totalItems = new List<NewsItem>();
            
            for (int pageIndex = firstPage + 1; remainingItemsCount > 0; pageIndex++)
            {
                IEnumerable<NewsItem> items = await getItems(
                    pageIndex,
                    cancellationToken);
                var itemsList = items.ToList();

                int itemsToTakeCount = GetItemsToTakeCount(
                    itemsList.Count,
                    remainingItemsCount);

                totalItems.AddRange(itemsList.Take(itemsToTakeCount));

                remainingItemsCount -= itemsToTakeCount;
                if (itemsList.Count < maxItemsPerPage) 
                {
                    // Meaning there are less than maximum in this page, which means this is for sure the last page
                    // (although the last page can contain 100 elements, in that case the loop will operate again and receive zero items
                    break;
                }
            }

            return totalItems;
        }
        
        private static int GetItemsToTakeCount(int receivedItemsCount, int remainingItemsCount)
        {
            return Math.Min(
                receivedItemsCount,
                remainingItemsCount);
        }
    }
}