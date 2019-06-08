# Dependency Injection with Azure Functions 

* This repository shows how to use Dependency Injection with Azure Functions.
* It similuates a task scheduler by querying Cosmos DB for all documents that have pending == 'true'
* It wil simply emit those documents to a Service Bus queue. 