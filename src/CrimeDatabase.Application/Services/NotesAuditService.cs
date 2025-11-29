using CrimeDatabase.Application.Interfaces.Repositories;
using CrimeDatabase.Application.Interfaces.Services;
using CrimeDatabase.Domain.Entities;

namespace CrimeDatabase.Application.Services
{
    /// <summary>
    /// Here we implement how the NotesAudit service should interact with the repository in order to
    /// fulfill the defined business logic in INotesAuditService.
    /// </summary>
    /// <param name="repo">The repository implementation to use.</param>
    public class NotesAuditService(INotesAuditRepository repo) : INotesAuditService
    {
        private readonly INotesAuditRepository _repo = repo;

        public Task<List<NotesAudit>> GetAllAsync() =>
            _repo.GetAllAsync();

        public Task<List<NotesAudit>> GetByCrimeIdAsync(Guid crimeId) =>
            _repo.GetByCrimeIdAsync(crimeId);
    }
}
