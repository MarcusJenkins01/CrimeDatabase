using CrimeDatabase.Domain.Entities;

namespace CrimeDatabase.Application.Interfaces.Services
{
    public interface INotesAuditService
    {
        Task<List<NotesAudit>> GetAllAsync();
        Task<List<NotesAudit>> GetByCrimeIdAsync(Guid crimeId);
    }
}
