namespace CrimeDatabase.Domain.Entities
{
    /// <summary>
    /// The entity used to store an individual change to the Notes field of a Crime entity.
    /// </summary>
    public class NotesAudit
    {
        public Guid Id { get; set; }
        public Guid CrimeId { get; set; }
        public string OriginalNotes { get; set; } = string.Empty;
        public string UpdatedNotes { get; set; } = string.Empty;
        public DateTime ChangedAt { get; set; }
        public Crime? Crime { get; set; }
    }
}
