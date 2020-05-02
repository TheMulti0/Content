using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Content.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Content.Services
{
    public class ContentDatabase
    {
        private readonly IMongoCollection<NewsItemEntity> _items;

        public ContentDatabase(IOptions<DatabaseSettings> options)
        {
            DatabaseSettings settings = options.Value;
            
            var client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.Database);

            _items = database.GetCollection<NewsItemEntity>(settings.Collection);
        }

        public Task<IAsyncCursor<NewsItemEntity>> GetAsync()
            => GetAsync(_ => true);

        private Task<IAsyncCursor<NewsItemEntity>> GetAsync(
            Expression<Func<NewsItemEntity, bool>> predicate)
        {
            return _items.FindAsync(predicate);
        }

        public Task AddAsync(NewsItemEntity item) 
            => _items.InsertOneAsync(item);

        public Task AddRangeAsync(IEnumerable<NewsItemEntity> items)
            => _items.InsertManyAsync(items);

        public Task RemoveAsync(NewsItemEntity item) 
            => _items.DeleteOneAsync(i => i.Id == item.Id);
    }
}