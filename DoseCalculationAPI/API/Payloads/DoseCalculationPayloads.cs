using System.ComponentModel.DataAnnotations;

namespace DoseCalculationAPI.API.Payloads;

/// <summary>REST request payload for POST /api/dose-calculations.</summary>
public sealed class DoseCalculationPostRequest
{
    [Required(ErrorMessage = "Medication name is required.")]
    public string Medication { get; init; } = string.Empty;

    [Range(typeof(decimal), "0.01", "700", ErrorMessage = "WeightKg must be greater than zero and below 700.")]
    public decimal WeightKg { get; init; }

    [Range(typeof(decimal), "0.01", "1000", ErrorMessage = "DosePerKg must be greater than zero and below 1000.")]
    public decimal DosePerKg { get; init; }

    [Range(typeof(decimal), "0.01", "100000", ErrorMessage = "MaxSingleDoseMg must be greater than zero and below 100 000.")]
    public decimal? MaxSingleDoseMg { get; init; }
}

/// <summary>REST response payload for POST /api/dose-calculations.</summary>
public sealed class DoseCalculationPostResponse
{
    public string Id { get; init; } = string.Empty;
    public string Medication { get; init; } = string.Empty;
    public decimal WeightKg { get; init; }
    public decimal DosePerKg { get; init; }
    public decimal? MaxSingleDoseMg { get; init; }
    public decimal CalculatedDoseMg { get; init; }
    public bool ExceedsSafeThreshold { get; init; }
    public DateTime CalculatedAtUtc { get; init; }
}

/// <summary>REST response payload for GET /api/dose-calculations and /api/dose-calculations/{id}.</summary>
public sealed class DoseCalculationGetResponse
{
    public string Id { get; init; } = string.Empty;
    public string Medication { get; init; } = string.Empty;
    public decimal WeightKg { get; init; }
    public decimal DosePerKg { get; init; }
    public decimal? MaxSingleDoseMg { get; init; }
    public decimal CalculatedDoseMg { get; init; }
    public bool ExceedsSafeThreshold { get; init; }
    public DateTime CalculatedAtUtc { get; init; }
}