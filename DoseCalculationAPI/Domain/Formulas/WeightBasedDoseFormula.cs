namespace DoseCalculationAPI.Domain.Formulas;

/// <summary>Standard weight-based dose formula: weight (kg) x dose per kg (mg).</summary>
internal sealed class WeightBasedDoseFormula : IDoseCalculationFormula
{
    public decimal Calculate(decimal weightKg, decimal dosePerKg)
    {
        return weightKg * dosePerKg;
    }
}