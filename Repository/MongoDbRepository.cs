using Domain;
using MongoDB.Driver;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Repository
{
    public class MongoDbRepository : IMongoDbRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MongoDbRepository(string mongoDbConnectionString, string mongoDbDatabaseName = null)
        {
            if (String.IsNullOrWhiteSpace(mongoDbDatabaseName))
            {
                mongoDbDatabaseName = mongoDbConnectionString.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            }

            Contract.Assert(!String.IsNullOrWhiteSpace(mongoDbDatabaseName));

            IMongoClient mongoClient = new MongoClient(mongoDbConnectionString);
            _mongoDatabase = mongoClient.GetDatabase(mongoDbDatabaseName);
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>() where TDocument : IAggregateRoot
        {
            return _mongoDatabase.GetCollection<TDocument>(typeof(TDocument).Name);
        }

        public void DropCollection<TDocument>() where TDocument : IAggregateRoot
        {
            _mongoDatabase.DropCollection(typeof(TDocument).Name);
        }

        public async Task<IList<TDocument>> Find<TDocument>(FilterDefinition<TDocument> filter = null) where TDocument : IAggregateRoot
        {
            if (filter == null)
            {
                var builder = Builders<TDocument>.Filter;
                filter = builder.Empty;
            }
            var cursor = await _mongoDatabase.GetCollection<TDocument>(typeof(TDocument).Name).FindAsync(filter);
            var response = new List<TDocument>();

            while (await cursor.MoveNextAsync())
            {
                response.AddRange(cursor.Current);
            }

            return response;
        }

        public async Task<TDocument> Get<TDocument>(string id) where TDocument : IAggregateRoot
        {
            return await GetCollection<TDocument>().AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Create<TDocument>(TDocument document) where TDocument : IAggregateRoot
        {
            await GetCollection<TDocument>().InsertOneAsync(document);
        }

        public async Task CreateMany<TDocument>(IList<TDocument> documents) where TDocument : IAggregateRoot
        {
            await GetCollection<TDocument>().InsertManyAsync(documents);
        }

        public async Task Replace<TDocument>(TDocument document) where TDocument : IAggregateRoot
        {
            await GetCollection<TDocument>().ReplaceOneAsync(it => it.Id == document.Id, document);
        }

        public async Task Delete<TDocument>(string id) where TDocument : IAggregateRoot
        {
            await GetCollection<TDocument>().DeleteOneAsync(it => it.Id == id);
        }

        public async Task Delete<TDocument>(TDocument document) where TDocument : IAggregateRoot
        {
            await GetCollection<TDocument>().DeleteOneAsync(it => it.Id == document.Id);
        }

        public async Task DeleteMany<TDocument>(FilterDefinition<TDocument> filter = null) where TDocument : IAggregateRoot
        {
            if (filter == null)
            {
                var builder = Builders<TDocument>.Filter;
                filter = builder.Empty;
            }

            await _mongoDatabase.GetCollection<TDocument>(typeof(TDocument).Name).DeleteManyAsync(filter);
        }
    }
}