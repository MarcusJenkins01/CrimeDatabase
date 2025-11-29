using CrimeDatabase.Domain.Entities;

namespace CrimeDatabase.Application.Interfaces.Services
{
    /// <summary>
    /// This defines the use-cases that should be implemented for the Crime service.
    /// In other words, this defines the business logic in a way that is well separated
    /// from the actual implementation.
    /// </summary>
    public interface ICrimeService
    {
        Task<List<Crime>> GetAllAsync();
        Task<Crime?> GetByIdAsync(Guid id);
        Task CreateAsync(Crime crime);
        Task UpdateNotesAsync(Guid crimeId, string newNotes);
    }
}
