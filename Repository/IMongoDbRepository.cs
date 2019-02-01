using Domain;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public interface IMongoDbRepository
    {
        IMongoCollection<TDocument> GetCollection<TDocument>() where TDocument : IAggregateRoot;

        void DropCollection<TDocument>() where TDocument : IAggregateRoot;

        Task<IList<TDocument>> Find<TDocument>(FilterDefinition<TDocument> filter = null) where TDocument : IAggregateRoot;

        Task<TDocument> Get<TDocument>(string id) where TDocument : IAggregateRoot;

        Task Create<TDocument>(TDocument document) where TDocument : IAggregateRoot;

        Task CreateMany<TDocument>(IList<TDocument> documents) where TDocument : IAggregateRoot;

        Task Replace<TDocument>(TDocument entity) where TDocument : IAggregateRoot;

        Task Delete<TDocument>(string id) where TDocument : IAggregateRoot;

        Task Delete<TDocument>(TDocument document) where TDocument : IAggregateRoot;

        Task DeleteMany<TDocument>(FilterDefinition<TDocument> filter = null) where TDocument : IAggregateRoot;
    }
}