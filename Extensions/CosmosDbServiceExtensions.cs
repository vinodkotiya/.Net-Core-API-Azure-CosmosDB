using onekarmaapi.Services;

namespace onekarmaapi.Extensions
{
    public static class CosmosDbServiceExtensions
    {
        public static async Task<UserService> InitializeCosmosClientInstanceAsync(this IConfigurationSection configurationSection)
        {
            var databaseName = configurationSection["DatabaseName"];
            var containerName = configurationSection["ContainerName"];
            var account = configurationSection["Account"];
            var key = configurationSection["Key"];

            var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            return new UserService(client, databaseName, containerName);
        }
    }
}