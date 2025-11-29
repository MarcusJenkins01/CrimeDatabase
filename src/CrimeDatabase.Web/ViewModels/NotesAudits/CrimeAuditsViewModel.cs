namespace CrimeDatabase.Web.ViewModels.NotesAudits
{
    /// <summary>
    /// The view model used to view a specific Crime's audits.
    /// </summary>
    public class CrimeAuditsViewModel
    {
        public Guid Id { get; set; }

        public List<IndexNotesAuditViewModel> NotesAudits { get; set; } = new();
    }
}
