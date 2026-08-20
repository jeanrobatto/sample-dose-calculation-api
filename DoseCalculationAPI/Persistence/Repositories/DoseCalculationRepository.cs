using DoseCalculationAPI.Persistence.Documents;
using Microsoft.Azure.Cosmos;

namespace DoseCalculationAPI.Persistence.Repositories;

/// <summary>Cosmos DB-backed implementation of IDoseCalculationRepository.</summary>
internal sealed class DoseCalculationRepository : IDoseCalculationRepository
{
    private readonly Container _container;

    public DoseCalculationRepository(CosmosClient cosmosClient, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(cosmosClient);
        ArgumentNullException.ThrowIfNull(configuration);

        var databaseName = configuration["CosmosDb:DatabaseName"];
        var containerName = configuration["CosmosDb:ContainerName"];
        _container = cosmosClient.GetContainer(databaseName, containerName);
    }

    public async Task<DoseCalculationDocument> CreateAsync(DoseCalculationDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);

        var response = await _container.CreateItemAsync(document, new PartitionKey(document.Medication));
        return response.Resource;
    }

    public async Task<DoseCalculationDocument?> GetByIdAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var query = new QueryDefinition("SELECT * FROM c WHERE c.id = @id")
            .WithParameter("@id", id);

        using var iterator = _container.GetItemQueryIterator<DoseCalculationDocument>(query);

        while (iterator.HasMoreResults)
        {
            var page = await iterator.ReadNextAsync();
            var match = page.FirstOrDefault();
            if (match is not null)
            {
                return match;
            }
        }

        return null;
    }

    public async Task<IReadOnlyList<DoseCalculationDocument>> GetAllAsync()
    {
        var query = new QueryDefinition("SELECT * FROM c");
        using var iterator = _container.GetItemQueryIterator<DoseCalculationDocument>(query);

        var results = new List<DoseCalculationDocument>();

        while (iterator.HasMoreResults)
        {
            var page = await iterator.ReadNextAsync();
            results.AddRange(page);
        }

        return results;
    }
}