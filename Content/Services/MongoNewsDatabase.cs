using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Content.Api;
using Content.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Content.Services
{
    public class MongoNewsDatabase : INewsDatabase
    {
        private readonly IMongoCollection<NewsItemEntity> _items;

        public MongoNewsDatabase(IOptions<MongoSettings> options)
        {
            var settings = options.Value;
            
            var client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.Database);

            _items = database.GetCollection<NewsItemEntity>(settings.Collection);
        }

        public async Task<IEnumerable<NewsItemEntity>> GetAsync()
        {
            IAsyncCursor<NewsItemEntity> cursor = await _items.FindAsync(_ => true);
            return await cursor.ToListAsync();
        }

        public async Task<IEnumerable<NewsItemEntity>> GetAsync(int maxResults, NewsSource[] excludedSources)
        {
            IAsyncCursor<NewsItemEntity> cursor = await _items
                .FindAsync(
                    item => excludedSources.All(source => item.Source != source),
                    new FindOptions<NewsItemEntity>
                    {
                        Limit = maxResults
                    });
            
            return await cursor.ToListAsync();
        }

        public Task AddAsync(NewsItemEntity item) 
            => _items.InsertOneAsync(item);

        public Task AddRangeAsync(IEnumerable<NewsItemEntity> items)
            => _items.InsertManyAsync(items);

        public Task RemoveAsync(NewsItemEntity item) 
            => _items.DeleteOneAsync(i => i.Id == item.Id);
    }
}