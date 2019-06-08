using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using TaskSchedulerService.Models;

namespace TaskSchedulerService
{
    public static class scheduler
    {
        [FunctionName("scheduler")]
        [return: ServiceBus("tasks", Connection = "SERVICEBUS_CONNECTIONSTRING")]
        public static string Run(
            [TimerTrigger("0 */2 * * * *")] TimerInfo timer, 
            [CosmosDB(
                databaseName: "testdb002",
                collectionName: "testcontainer002",
                ConnectionStringSetting = "COSMOS_CONNECTIONSTRING",
                PartitionKey = "001",
                SqlQuery = "SELECT * FROM c where c.pending = 'true'")]
                IEnumerable<MigrationTask> tasks,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed with 'SELECT * FROM c where c.pending = 'true''");
            return (JsonConvert.SerializeObject(tasks));
        }
    }
}
