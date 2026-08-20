namespace DoseCalculationAPI.Domain.Dtos;

/// <summary>Domain representation of a dose calculation. Always valid once constructed.</summary>
public sealed class DoseCalculation
{
    public string Id { get; }
    public string Medication { get; }
    public decimal WeightKg { get; }
    public decimal DosePerKg { get; }
    public decimal? MaxSingleDoseMg { get; }
    public decimal CalculatedDoseMg { get; }
    public bool ExceedsSafeThreshold { get; }
    public DateTime CalculatedAtUtc { get; }

    private DoseCalculation(
        string medication,
        decimal weightKg,
        decimal dosePerKg,
        decimal? maxSingleDoseMg,
        decimal calculatedDoseMg,
        bool exceedsSafeThreshold)
    {
        Id = Guid.NewGuid().ToString();
        Medication = medication;
        WeightKg = weightKg;
        DosePerKg = dosePerKg;
        MaxSingleDoseMg = maxSingleDoseMg;
        CalculatedDoseMg = calculatedDoseMg;
        ExceedsSafeThreshold = exceedsSafeThreshold;
        CalculatedAtUtc = DateTime.UtcNow;
    }

    /// <summary>Validates inputs and constructs a DoseCalculation from an already-computed dose.</summary>
    /// <param name="medication">Medication name. Must not be null/empty.</param>
    /// <param name="weightKg">Patient weight in kg. Must be greater than zero.</param>
    /// <param name="dosePerKg">Dose per kg in mg. Must be greater than zero.</param>
    /// <param name="maxSingleDoseMg">Optional max safe single dose in mg.</param>
    /// <param name="calculatedDoseMg">The total dose, already computed by a formula.</param>
    /// <returns>A valid, immutable DoseCalculation.</returns>
    /// <exception cref="ArgumentException">Thrown when any input is invalid.</exception>
    public static DoseCalculation Create(
        string medication,
        decimal weightKg,
        decimal dosePerKg,
        decimal? maxSingleDoseMg,
        decimal calculatedDoseMg)
    {
        if (string.IsNullOrWhiteSpace(medication))
        {
            throw new ArgumentException("Medication name is required.", nameof(medication));
        }

        if (weightKg <= 0)
        {
            throw new ArgumentException("WeightKg must be greater than zero.", nameof(weightKg));
        }

        if (dosePerKg <= 0)
        {
            throw new ArgumentException("DosePerKg must be greater than zero.", nameof(dosePerKg));
        }

        var exceedsSafeThreshold = maxSingleDoseMg.HasValue && calculatedDoseMg > maxSingleDoseMg.Value;

        return new DoseCalculation(medication, weightKg, dosePerKg, maxSingleDoseMg, calculatedDoseMg, exceedsSafeThreshold);
    }
}