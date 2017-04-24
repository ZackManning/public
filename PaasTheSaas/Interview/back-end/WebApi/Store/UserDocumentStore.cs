using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace WebApi.Store
{
    public class UserDocumentStore : IUserDataStore
    {
        protected readonly string databaseID;
        protected readonly string collectionID;
        protected DocumentClient client;

        public UserDocumentStore(string endpoint, string authKey, string databaseID, string collectionID)
        {
            client = new DocumentClient(new Uri(endpoint), authKey, new ConnectionPolicy()
            {
                EnableEndpointDiscovery = false
            });
            this.databaseID = databaseID;
            this.collectionID = collectionID;

            Connect(null).Wait();
        }

        public async Task Connect(string connectionInfo)
        {
            await client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(databaseID),
                new DocumentCollection()
                {
                    Id = collectionID
                },
                new RequestOptions()
                {
                    OfferThroughput = 1000
                });
        }

        public async Task Delete(string id)
        {
            await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(databaseID, collectionID, id));
        }

        public async Task<IEnumerable<T>> Get<T>()
        {
            return await Query<T>(null);
        }

        public async Task<T> Get<T>(string id)
        {
            var response = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseID, collectionID, id));
            return (T)(dynamic)response.Resource;
        }

        public async Task<T> Insert<T>(T document)
        {
            var response = await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseID, collectionID), document);
            return (T)(dynamic)response;
        }

        public async Task<IEnumerable<T>> Query<T>(Func<T, bool> predicate)
        {
            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(databaseID, collectionID),
                new FeedOptions() { MaxItemCount = -1 })
                .Where(predicate)
                .AsDocumentQuery();
            var results = new List<T>();
            while (query.HasMoreResults)
            {
                var queryResponse = await query.ExecuteNextAsync<T>();
                results.AddRange(queryResponse);
            }
            return results;
        }

        public async Task Update<T>(string id, T document)
        {
            await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseID, collectionID, id), document);
        }
    }
}
