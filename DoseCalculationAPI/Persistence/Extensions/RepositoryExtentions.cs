using DoseCalculationAPI.Persistence.Repositories;

namespace DoseCalculationAPI.Persistence.Extensions;

/// <summary>Registers repository implementations for dependency injection.</summary>
internal static class RepositoryServiceExtensions
{
    /// <summary>Registers IDoseCalculationRepository and its Cosmos-backed implementation.</summary>
    /// <param name="services">The service collection to add the repository to.</param>
    /// <returns>The same service collection, for chaining.</returns>
    public static IServiceCollection AddDoseCalculationRepository(this IServiceCollection services)
    {
        services.AddScoped<IDoseCalculationRepository, DoseCalculationRepository>();
        return services;
    }
}