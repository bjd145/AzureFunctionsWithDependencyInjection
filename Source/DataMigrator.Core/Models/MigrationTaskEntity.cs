using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataMigrator.Core.Models
{
    public class MigrationTask
    {
        public string PartitionKey { get; set;}
        public string id { get; set; }
        public string pending { get; set; }
        public string jobName { get; set; }
    }
}