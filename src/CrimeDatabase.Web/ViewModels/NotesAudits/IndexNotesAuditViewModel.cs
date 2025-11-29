using System.ComponentModel.DataAnnotations;

namespace CrimeDatabase.Web.ViewModels.NotesAudits
{
    /// <summary>
    /// View Model for viewing the notes audits on the Index page.
    /// </summary>
    public class IndexNotesAuditViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Crime Id")]
        public Guid CrimeId { get; set; }

        [Display(Name = "Original Notes")]
        public string OriginalNotes {  get; set; } = string.Empty;

        [Display(Name = "Updated Notes")]
        public string UpdatedNotes { get; set; } = string.Empty;

        [Display(Name = "Changed At")]
        public DateTime ChangedAt { get; set; }
    }
}
