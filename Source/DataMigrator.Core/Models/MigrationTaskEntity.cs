using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataMigrator.Core.Models
{
    public class MigrationTask : Entity
    {
        public string PartitionKey { get; set;}
        public string Status { get; set; }
        public string JobName { get; set; }
    }
}