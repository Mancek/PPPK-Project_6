using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace PersonManager.Dao
{
    public static class CosmosDbServiceProvider
    {
        private const string DatabaseName = "People";
        private const string ContainerName = "People";
        private const string Account = "https://people-cosmos.documents.azure.com:443/";
        private const string Key = "lNLXNrcMXIqbk4ZCcb3axcpFcwWsaDiLcmR8VA30y1Y5C2wY9YugqoeAkeu0JbJhImZuMMZqV6KDQjjrvBeeRw==";
        private static ICosmosDbService cosmosDbService;

        public static ICosmosDbService CosmosDbService { get => cosmosDbService; }

        public async static Task Init()
        {
            CosmosClient client = new CosmosClient(Account, Key);
            cosmosDbService = new CosmosDbService(client, DatabaseName, ContainerName);
            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(DatabaseName);
            await database.Database.CreateContainerIfNotExistsAsync(ContainerName, "/id");
        }
    }
}