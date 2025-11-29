using CrimeDatabase.Application.Interfaces.Repositories;
using CrimeDatabase.Application.Interfaces.Services;
using CrimeDatabase.Domain.Entities;

namespace CrimeDatabase.Application.Services
{
    /// <summary>
    /// Here we implement how the Crime service should interact with the repository in order to
    /// fulfill the defined business logic in ICrimeService. This doesn't rely on Entity Framework
    /// to allow better separation and allows us to use a mock repository for unit testing.
    /// Infrastructure then uses these service implementations with the real DbContext as the repository.
    /// </summary>
    /// <param name="repo">The repository to use.</param>
    public class CrimeService(ICrimeRepository repo) : ICrimeService
    {
        private readonly ICrimeRepository _repo = repo;

        public Task<List<Crime>> GetAllAsync() => _repo.GetAllAsync();

        public Task<Crime?> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);

        public async Task CreateAsync(Crime crime)
        {
            await _repo.AddAsync(crime);
            await _repo.SaveChangesAsync();
        }

        public async Task UpdateNotesAsync(Guid crimeId, string newNotes)
        {
            var crime = await _repo.GetByIdAsync(crimeId);
            if (crime == null) return;

            var audit = new NotesAudit
            {
                Id = Guid.NewGuid(),
                CrimeId = crimeId,
                OriginalNotes = crime.Notes,
                UpdatedNotes = newNotes,
                ChangedAt = DateTime.UtcNow
            };

            crime.Notes = newNotes;

            _repo.Update(crime);
            await _repo.AddAuditAsync(audit);
            await _repo.SaveChangesAsync();
        }
    }
}
