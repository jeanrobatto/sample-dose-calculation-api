using DoseCalculationAPI.Domain.Dtos;

namespace DoseCalculationAPI.Domain.Services;

/// <summary>Business logic for computing dose calculations.</summary>
public interface IDoseCalculationService
{
    /// <summary>Validates inputs and computes a dose calculation.</summary>
    /// <param name="medication">Medication name.</param>
    /// <param name="weightKg">Patient weight in kg.</param>
    /// <param name="dosePerKg">Dose per kg in mg.</param>
    /// <param name="maxSingleDoseMg">Optional max safe single dose in mg.</param>
    /// <returns>A valid, computed DoseCalculation.</returns>
    /// <exception cref="ArgumentException">Thrown when any input is invalid.</exception>
    DoseCalculation Calculate(string medication, decimal weightKg, decimal dosePerKg, decimal? maxSingleDoseMg);
}