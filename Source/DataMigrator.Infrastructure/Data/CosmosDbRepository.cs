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
    public class CosmosDbRespository : ICosmosDbRespository<MigrationTask>
    {

        private ICosmosDbClient _cosmosDbClient;
        private static string query = "select * from c where c.pending = 'true'";
        private PartitionKey _partitionKey = new PartitionKey("Config");

        public CosmosDbRespository(ICosmosDbClient cosmosDbClient)
        {
            _cosmosDbClient = cosmosDbClient;
        }

        public IList<MigrationTask> GetPending()
        {
            var tasks = new List<MigrationTask>();
            var documents = _cosmosDbClient.ReadDocuments(query, new FeedOptions()
            {
                PartitionKey = ResolvePartitionKey()
            });

            foreach( var document in documents ) {
               tasks.Add((MigrationTask)(dynamic) document);
            } 

            return tasks;
        }

        public Task<MigrationTask> GetByIdAsync(string id){
            throw new NotImplementedException();
        }

        public Task<MigrationTask> AddAsync(MigrationTask entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(MigrationTask entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(MigrationTask entity)
        {
            throw new NotImplementedException();
        }
        public string GenerateId(MigrationTask entity) => Guid.NewGuid().ToString();
        public PartitionKey ResolvePartitionKey(string entityId) => _partitionKey;
        public PartitionKey ResolvePartitionKey() => _partitionKey;
    }
}
