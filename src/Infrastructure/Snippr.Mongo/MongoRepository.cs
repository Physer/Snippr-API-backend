using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Snippr.Application.Data;
using Snippr.Application.Options;
using Snippr.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Snippr.Mongo
{
    public class MongoRepository : IRepository, IDataDependency
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _snipprDatabase;

        public MongoRepository(IOptions<MongoOptions> mongoOptions)
        {
            if (string.IsNullOrWhiteSpace(mongoOptions?.Value?.ConnectionString))
                throw new ArgumentNullException(nameof(mongoOptions.Value.ConnectionString), "No MongoDB connection string located");

            _mongoClient = new MongoClient(mongoOptions.Value.ConnectionString);
            _snipprDatabase = _mongoClient.GetDatabase(DataConstants.Database);
        }

        public async Task WriteSnippet(Snippet snippet)
        {
            var snippets = GetSnippetCollection();
            await snippets.InsertOneAsync(snippet);
        }

        public async Task<Snippet> GetSnippet(Guid snippetId)
        {
            var snippets = GetSnippetCollection();
            return (await snippets.FindAsync(x => x.Identifier.Equals(snippetId))).FirstOrDefault();
        }

        public async Task DeleteSnippet(Guid snippetId)
        {
            var snippets = GetSnippetCollection();
            await snippets.DeleteOneAsync(x => x.Identifier.Equals(snippetId));
        }

        public async Task UpdateSnippet(Guid snippetId, Snippet newSnippet)
        {
            var snippet = GetSnippet(snippetId);
            if (snippet is null)
                return;

            var snippets = GetSnippetCollection();

            var filter = Builders<Snippet>.Filter.Eq(x => x.Identifier, snippetId);
            var updateBuilder = Builders<Snippet>.Update.Set(x => x, newSnippet);
            await snippets.UpdateOneAsync(filter, updateBuilder);
        }

        private IMongoCollection<Snippet> GetSnippetCollection() => _snipprDatabase.GetCollection<Snippet>(DataConstants.SnippetCollection);
    }
}
