using CrimeDatabase.Domain.Entities;

namespace CrimeDatabase.Application.Interfaces.Repositories
{
    /// <summary>
    /// The repository interface for the NotesAudit entity.
    /// </summary>
    public interface INotesAuditRepository
    {
        Task<List<NotesAudit>> GetAllAsync();
        Task<List<NotesAudit>> GetByCrimeIdAsync(Guid crimeId);
    }
}
