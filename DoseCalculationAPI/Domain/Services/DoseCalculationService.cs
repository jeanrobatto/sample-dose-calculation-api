using DoseCalculationAPI.Domain.Dtos;
using DoseCalculationAPI.Domain.Formulas;

namespace DoseCalculationAPI.Domain.Services;

/// <summary>Default implementation of dose calculation business logic.</summary>
internal sealed class DoseCalculationService : IDoseCalculationService
{
    private readonly IDoseCalculationFormula _formula;

    public DoseCalculationService(IDoseCalculationFormula formula)
    {
        ArgumentNullException.ThrowIfNull(formula);
        _formula = formula;
    }

    public DoseCalculation Calculate(string medication, decimal weightKg, decimal dosePerKg, decimal? maxSingleDoseMg)
    {
        var calculatedDoseMg = _formula.Calculate(weightKg, dosePerKg);
        return DoseCalculation.Create(medication, weightKg, dosePerKg, maxSingleDoseMg, calculatedDoseMg);
    }
}