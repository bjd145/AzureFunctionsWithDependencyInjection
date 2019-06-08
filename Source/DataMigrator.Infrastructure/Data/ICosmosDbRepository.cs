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
    public interface ICosmosDbRespository<T>
    {
        IList<T> GetByQuery(string query);
        Task<T> GetByIdAsync(string id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        string GenerateId(T entity);
        PartitionKey ResolvePartitionKey();
    }
}
