using System.ComponentModel.DataAnnotations;
using CrimeDatabase.Domain.Enums;

namespace CrimeDatabase.Web.ViewModels.Crimes
{
    /// <summary>
    /// The view model for the Crime entity used for the Create view.
    /// </summary>
    public class CreateCrimeViewModel
    {
        [Required]
        [Display(Name = "Crime Date")]
        [DataType(DataType.Date)]
        public DateOnly CrimeDate { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Display(Name = "Victim Name")]
        public string VictimName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Crime Type")]
        public CrimeType CrimeType { get; set; }

        [Display(Name = "Notes")]
        [StringLength(2000)]
        public string Notes { get; set; } = string.Empty;
    }
}
