using CrimeDatabase.Domain.Enums;

namespace CrimeDatabase.Domain.Entities
{
    /// <summary>
    /// The domain entity for a recorded crime.
    /// Validation or UI annotations aren't provided here to separate models entirely from the UI logic.
    /// </summary>
    public class Crime
    {
        public Guid Id { get; set; }

        public DateOnly CrimeDate { get; set; }

        public string Location { get; set; } = string.Empty;

        public string VictimName { get; set; } = string.Empty;

        public CrimeType CrimeType { get; set; }

        public string Notes { get; set; } = string.Empty;

        /// <summary>
        /// Collection of the audits for the notes field for this crime.
        /// Used to retrieve the audits for a particular crime more easily.
        /// </summary>
        public List<NotesAudit> NotesAudits { get; set; } = new();
    }
}
