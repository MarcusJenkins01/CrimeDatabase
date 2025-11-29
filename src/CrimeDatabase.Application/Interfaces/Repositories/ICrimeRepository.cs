using CrimeDatabase.Domain.Entities;

namespace CrimeDatabase.Application.Interfaces.Repositories
{
    /// <summary>
    /// Using repository interfaces allows us to add abstraction for data access. The main benefit
    /// of this is that it allows us to do unit testing with a mock DbContext without rather than the
    /// real one without having to change the implementation code.
    /// </summary>
    public interface ICrimeRepository
    {
        Task<List<Crime>> GetAllAsync();
        Task<Crime?> GetByIdAsync(Guid id);

        Task AddAsync(Crime crime);
        void Update(Crime crime);

        Task AddAuditAsync(NotesAudit audit);

        Task SaveChangesAsync();
    }
}
