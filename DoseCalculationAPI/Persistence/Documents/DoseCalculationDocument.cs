using Newtonsoft.Json;
using DoseCalculationAPI.Domain.Dtos;

namespace DoseCalculationAPI.Persistence.Documents;

/// <summary>Cosmos DB document shape for a persisted dose calculation. Partitioned by Medication.</summary>
public sealed class DoseCalculationDocument
{
    [JsonProperty("id")]
    public string Id { get; init; } = string.Empty;

    [JsonProperty("medication")]
    public string Medication { get; init; } = string.Empty;

    [JsonProperty("weightKg")]
    public decimal WeightKg { get; init; }

    [JsonProperty("dosePerKg")]
    public decimal DosePerKg { get; init; }

    [JsonProperty("maxSingleDoseMg")]
    public decimal? MaxSingleDoseMg { get; init; }

    [JsonProperty("calculatedDoseMg")]
    public decimal CalculatedDoseMg { get; init; }

    [JsonProperty("exceedsSafeThreshold")]
    public bool ExceedsSafeThreshold { get; init; }

    [JsonProperty("calculatedAtUtc")]
    public DateTime CalculatedAtUtc { get; init; }

    /// <summary>Maps a validated domain DoseCalculation into its Cosmos document shape.</summary>
    /// <param name="calculation">The domain object to map from.</param>
    /// <returns>A Cosmos-ready document.</returns>
    public static DoseCalculationDocument FromDomain(DoseCalculation calculation)
    {
        ArgumentNullException.ThrowIfNull(calculation);

        return new DoseCalculationDocument
        {
            Id = calculation.Id,
            Medication = calculation.Medication,
            WeightKg = calculation.WeightKg,
            DosePerKg = calculation.DosePerKg,
            MaxSingleDoseMg = calculation.MaxSingleDoseMg,
            CalculatedDoseMg = calculation.CalculatedDoseMg,
            ExceedsSafeThreshold = calculation.ExceedsSafeThreshold,
            CalculatedAtUtc = calculation.CalculatedAtUtc
        };
    }
}