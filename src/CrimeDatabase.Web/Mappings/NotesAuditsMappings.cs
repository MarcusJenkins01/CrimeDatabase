using CrimeDatabase.Domain.Entities;
using CrimeDatabase.Web.ViewModels.NotesAudits;

namespace CrimeDatabase.Web.Mappings
{
    public static class NotesAuditsMappings
    {
        public static IndexNotesAuditViewModel ToIndexViewModel(this NotesAudit audit)
        {
            return new IndexNotesAuditViewModel()
            {
                Id = audit.Id,
                CrimeId = audit.CrimeId,
                OriginalNotes = audit.OriginalNotes,
                UpdatedNotes = audit.UpdatedNotes,
                ChangedAt = audit.ChangedAt
            };
        }
    }
}
