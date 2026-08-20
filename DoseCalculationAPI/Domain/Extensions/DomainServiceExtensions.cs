using DoseCalculationAPI.Domain.Formulas;
using DoseCalculationAPI.Domain.Services;

namespace DoseCalculationAPI.Domain.Extensions;

/// <summary>Registers domain service implementations for dependency injection.</summary>
internal static class DomainServiceExtensions
{
    /// <summary>Registers IDoseCalculationService, IDoseCalculationFormula, and their default implementations.</summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <returns>The same service collection, for chaining.</returns>
    public static IServiceCollection AddDoseCalculationDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IDoseCalculationFormula, WeightBasedDoseFormula>();
        services.AddScoped<IDoseCalculationService, DoseCalculationService>();
        return services;
    }
}