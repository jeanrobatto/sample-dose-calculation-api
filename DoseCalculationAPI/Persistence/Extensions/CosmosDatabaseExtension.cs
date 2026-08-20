using Microsoft.Azure.Cosmos;

namespace DoseCalculationAPI.Persistence.Extensions;

/// <summary>Registers and initializes the Cosmos DB client and resources.</summary>
internal static class CosmosDatabaseExtensions
{
    /// <summary>Registers a singleton CosmosClient using "CosmosDb:ConnectionString" config.</summary>
    /// <param name="services">The service collection to add the client to.</param>
    /// <param name="configuration">App configuration containing the CosmosDb section.</param>
    /// <returns>The same service collection, for chaining.</returns>
    public static IServiceCollection AddCosmosDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(sp =>
        {
            var connectionString = configuration["CosmosDb:ConnectionString"];
            var clientOptions = new CosmosClientOptions
            {
                // Needed for an unsecured HTTP emulator connection.
                // If this was a real app with a real database instance, we would use Direct mode for better performance.
                ConnectionMode = ConnectionMode.Gateway
            };
            return new CosmosClient(connectionString, clientOptions);
        });

        return services;
    }

    /// <summary>Creates the Cosmos database/container if they don't exist. Can be called once at startup, before app.Run().</summary>
    /// <param name="app">The built WebApplication instance.</param>
    /// <returns>A task representing the async initialization.</returns>
    public static async Task InitializeCosmosDbAsync(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        using var scope = app.Services.CreateScope();
        var cosmosClient = scope.ServiceProvider.GetRequiredService<CosmosClient>();
        var config = app.Configuration;

        var database = await cosmosClient.CreateDatabaseIfNotExistsAsync(config["CosmosDb:DatabaseName"]);
        await database.Database.CreateContainerIfNotExistsAsync(
            config["CosmosDb:ContainerName"], "/medication");
    }
}