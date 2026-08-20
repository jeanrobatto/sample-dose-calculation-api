using DoseCalculationAPI.API.Payloads;
using DoseCalculationAPI.Domain.Dtos;
using DoseCalculationAPI.Domain.Services;
using DoseCalculationAPI.Persistence.Documents;
using DoseCalculationAPI.Persistence.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace DoseCalculationAPI.API.Controllers;

[ApiController]
[Route("api/dose-calculations")]
public class DoseCalculationsController : ControllerBase
{
    private readonly IDoseCalculationService _doseCalculationService;
    private readonly IDoseCalculationRepository _repository;

    public DoseCalculationsController(IDoseCalculationService doseCalculationService, IDoseCalculationRepository repository)
    {
        ArgumentNullException.ThrowIfNull(doseCalculationService);
        ArgumentNullException.ThrowIfNull(repository);

        _doseCalculationService = doseCalculationService;
        _repository = repository;
    }

    /// <summary>Calculates and persists a dose calculation.</summary>
    [HttpPost]
    public async Task<ActionResult<DoseCalculationPostResponse>> CreateDoseCalculation([FromBody] DoseCalculationPostRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        DoseCalculation calculation;

        try
        {
            calculation = _doseCalculationService.Calculate(request.Medication, request.WeightKg, request.DosePerKg, request.MaxSingleDoseMg);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

        var document = DoseCalculationDocument.FromDomain(calculation);
        var saved = await _repository.CreateAsync(document);
        var response = MapToResponse(saved);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DoseCalculationGetResponse>> GetById(string id)
    {
        var document = await _repository.GetByIdAsync(id);

        if (document is null)
        {
            return NotFound();
        }

        return Ok(MapToResponse(document));
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DoseCalculationGetResponse>>> GetAll()
    {
        var documents = await _repository.GetAllAsync();
        return Ok(documents.Select(MapToResponse).ToList());
    }

    private static DoseCalculationPostResponse MapToResponse(DoseCalculationDocument document)
    {
        return new DoseCalculationPostResponse
        {
            Id = document.Id,
            Medication = document.Medication,
            WeightKg = document.WeightKg,
            DosePerKg = document.DosePerKg,
            MaxSingleDoseMg = document.MaxSingleDoseMg,
            CalculatedDoseMg = document.CalculatedDoseMg,
            ExceedsSafeThreshold = document.ExceedsSafeThreshold,
            CalculatedAtUtc = document.CalculatedAtUtc
        };
    }
}