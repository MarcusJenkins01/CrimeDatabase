using CrimeDatabase.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CrimeDatabase.Web.ViewModels.Crimes
{
    /// <summary>
    /// The view model for the Crime entity used for the Index view.
    /// </summary>
    public class IndexCrimeViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Crime Date")]
        public DateOnly CrimeDate { get; set; }

        public string Location { get; set; } = string.Empty;

        [Display(Name = "Victim Name")]
        public string VictimName { get; set; } = string.Empty;

        [Display(Name = "Crime Type")]
        public CrimeType CrimeType { get; set; }

        public string Notes { get; set; } = string.Empty;

        // This maps the enums to a more user-friendly display name.
        public string CrimeTypeDisplay =>
            CrimeType switch
            {
                CrimeType.AntiSocialBehaviour => "Anti-Social Behaviour",
                CrimeType.CriminalDamage => "Criminal Damage",
                _ => CrimeType.ToString()
            };

        // Convert to an ordinal date string format as specified in the task brief.
        public string CrimeDateDisplay
        {
            get
            {
                int day = CrimeDate.Day;

                string suffix = (day % 10) switch
                {
                    1 when day != 11 => "st",
                    2 when day != 12 => "nd",
                    3 when day != 13 => "rd",
                    _ => "th"
                };

                return $"{day}{suffix} {CrimeDate:MMMM yyyy}";
            }
        }

    }
}
