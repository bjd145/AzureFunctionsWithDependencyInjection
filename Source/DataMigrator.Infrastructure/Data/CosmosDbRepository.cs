using System;
using System.Net;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using DataMigrator.Core.Models;

namespace DataMigrator.Intrastructure.Data
{
    public class CosmosDbRespository<T> : ICosmosDbRespository<T> where T : MigrationTask
    {

        private ICosmosDbClient _cosmosDbClient;
        private string _partitionKey;

        public CosmosDbRespository(ICosmosDbClient cosmosDbClient, string partitionKey)
        {
            _cosmosDbClient = cosmosDbClient;
            _partitionKey = partitionKey;
        }

        public IList<T> GetByQuery(string query)
        {
            var tasks = new List<T>();

            var documents = _cosmosDbClient.ReadDocumentsByQuery(query, new FeedOptions()
            {
                PartitionKey = ResolvePartitionKey()
            });

            foreach( var document in documents ) {
               tasks.Add((T)(dynamic) document);
            } 

            return tasks;
        }

        public Task<T> GetByIdAsync(string id){
            throw new NotImplementedException();
        }

        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }
        public string GenerateId(T entity) => Guid.NewGuid().ToString();
        public PartitionKey ResolvePartitionKey() => new PartitionKey(_partitionKey);
    }
}
