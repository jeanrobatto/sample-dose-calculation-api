namespace DoseCalculationAPI.Domain.Formulas;

/// <summary>Computes a total dose in mg from patient weight and dose-per-kg.</summary>
public interface IDoseCalculationFormula
{
    /// <summary>Calculates the total dose.</summary>
    /// <param name="weightKg">Patient weight in kg.</param>
    /// <param name="dosePerKg">Dose per kg in mg.</param>
    /// <returns>The calculated total dose in mg.</returns>
    decimal Calculate(decimal weightKg, decimal dosePerKg);
}