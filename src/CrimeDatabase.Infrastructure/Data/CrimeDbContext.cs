using CrimeDatabase.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrimeDatabase.Infrastructure.Data
{
    /// <summary>
    /// Here we define the DbContext that handles storage for the Crime and NotesAudits entities,
    /// using Entity Framework. I have defined the validation for the entities here rather than
    /// using data annotation attributes to separate the database storage details from the core
    /// domain entities/models for better separation of concerns.
    /// </summary>
    public class CrimeDbContext(DbContextOptions<CrimeDbContext> options) : DbContext(options)
    {
        public DbSet<Crime> Crimes => Set<Crime>();
        public DbSet<NotesAudit> NotesAudits => Set<NotesAudit>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Crime can have many NotesAudits (one-to-many), so CrimeId is the foreign key for NotesAudit.
            builder.Entity<Crime>()
                .HasMany(c => c.NotesAudits)
                .WithOne(a => a.Crime)
                .HasForeignKey(a => a.CrimeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Crime entity validations.
            builder.Entity<Crime>()
                .Property(c => c.Location)
                .HasMaxLength(200)
                .IsRequired();

            builder.Entity<Crime>()
                .Property(c => c.VictimName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Entity<Crime>()
                .Property(c => c.Notes)
                .HasMaxLength(2000);

            // NotesAudit entity validations.
            builder.Entity<NotesAudit>()
                .Property(a => a.OriginalNotes)
                .HasMaxLength(2000);

            builder.Entity<NotesAudit>()
                .Property(a => a.UpdatedNotes)
                .HasMaxLength(2000);
        }
    }
}

