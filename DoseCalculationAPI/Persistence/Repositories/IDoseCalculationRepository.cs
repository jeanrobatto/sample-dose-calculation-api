using DoseCalculationAPI.Persistence.Documents;

namespace DoseCalculationAPI.Persistence.Repositories;

/// <summary>Persistence abstraction for dose calculation documents.</summary>
public interface IDoseCalculationRepository
{
    /// <summary>Persists a dose calculation document to the store.</summary>
    /// <param name="document">The document to persist.</param>
    /// <returns>The persisted document as returned by the store.</returns>
    Task<DoseCalculationDocument> CreateAsync(DoseCalculationDocument document);

    /// <summary>Retrieves a single dose calculation document by id.</summary>
    /// <param name="id">The document id to look up.</param>
    /// <returns>The matching document, or null if not found.</returns>
    Task<DoseCalculationDocument?> GetByIdAsync(string id);

    /// <summary>Retrieves all dose calculation documents.</summary>
    /// <returns>All persisted documents.</returns>
    Task<IReadOnlyList<DoseCalculationDocument>> GetAllAsync();
}