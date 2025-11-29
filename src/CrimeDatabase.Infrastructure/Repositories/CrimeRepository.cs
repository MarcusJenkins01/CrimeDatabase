using CrimeDatabase.Application.Interfaces.Repositories;
using CrimeDatabase.Domain.Entities;
using CrimeDatabase.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CrimeDatabase.Infrastructure.Repositories
{
    /// <summary>
    /// This code implements the crime repository using Entity Framework from the interface in Application.
    /// </summary>
    public class CrimeRepository(CrimeDbContext context) : ICrimeRepository
    {
        private readonly CrimeDbContext _context = context;

        public async Task<List<Crime>> GetAllAsync()
        {
            return await _context.Crimes
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Crime?> GetByIdAsync(Guid id)
        {
            return _context.Crimes
                .Include(c => c.NotesAudits)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Crime crime)
        {
            await _context.Crimes.AddAsync(crime);
        }

        public void Update(Crime crime)
        {
            _context.Crimes.Update(crime);
        }

        public async Task AddAuditAsync(NotesAudit audit)
        {
            await _context.NotesAudits.AddAsync(audit);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
