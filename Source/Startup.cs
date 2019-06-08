using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using DataMigrator.Core.Models;
using DataMigrator.Core.Helpers;
using DataMigrator.Intrastructure.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

[assembly: FunctionsStartup(typeof(DataMigrator.Startup))]

namespace DataMigrator
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable("COSMOS_CONNECTIONSTRING", EnvironmentVariableTarget.Process);
            var databaseName     = Environment.GetEnvironmentVariable("COSMOS_DATABASENAME", EnvironmentVariableTarget.Process);
            var collectionName   = Environment.GetEnvironmentVariable("COSMOS_COLLECTIONNAME", EnvironmentVariableTarget.Process);
            var partitionKey     = Environment.GetEnvironmentVariable("COSMOS_PARTITIONKEY", EnvironmentVariableTarget.Process);

            var cosmosDBConnectionString = new CosmosDBConnectionString(connectionString);

            var documentClient = new DocumentClient(cosmosDBConnectionString.ServiceEndpoint, cosmosDBConnectionString.AuthKey, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            documentClient.OpenAsync().Wait();

            builder.Services.AddSingleton<ICosmosDbRespository<MigrationTask>>(
                new CosmosDbRespository<MigrationTask>(
                    new CosmosDbClient(databaseName, collectionName, documentClient),
                    partitionKey
                )
            );
        }
    }
}
