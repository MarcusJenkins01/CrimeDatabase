using System.ComponentModel.DataAnnotations;

namespace CrimeDatabase.Web.ViewModels.Crimes
{
    /// <summary>
    /// The view model used to edit a crime's notes.
    /// </summary>
    public class EditCrimeViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(2000)]
        public string Notes { get; set; } = string.Empty;
    }
}
