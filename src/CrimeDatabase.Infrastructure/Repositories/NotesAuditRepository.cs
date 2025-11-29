using CrimeDatabase.Application.Interfaces.Repositories;
using CrimeDatabase.Domain.Entities;
using CrimeDatabase.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace CrimeDatabase.Infrastructure.Repositories
{
    /// <summary>
    /// This implements the audit repository using Entity Framework from the interface in Application.
    /// </summary>
    public class NotesAuditRepository(CrimeDbContext context) : INotesAuditRepository
    {
        private readonly CrimeDbContext _context = context;

        public Task<List<NotesAudit>> GetAllAsync() =>
            _context.NotesAudits
                .AsNoTracking()
                .OrderByDescending(a => a.ChangedAt)
                .ToListAsync();

        public Task<List<NotesAudit>> GetByCrimeIdAsync(Guid crimeId) =>
            _context.NotesAudits
                .AsNoTracking()
                .Where(a => a.CrimeId == crimeId)
                .OrderByDescending(a => a.ChangedAt)
                .ToListAsync();
    }
}
