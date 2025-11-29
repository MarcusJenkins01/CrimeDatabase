using CrimeDatabase.Domain.Entities;
using CrimeDatabase.Web.ViewModels.Crimes;

namespace CrimeDatabase.Web.Mappings
{
    /// <summary>
    /// I define an extension method to map a Crime to a view model to reduce code repetition while avoid editing the 
    /// original domain entity class definition for better separation of concerns between projects.
    /// </summary>
    public static class CrimeMappings
    {
        public static IndexCrimeViewModel ToIndexViewModel(this Crime crime)
        {
            return new IndexCrimeViewModel()
            {
                Id = crime.Id,
                CrimeDate = crime.CrimeDate,
                Location = crime.Location,
                VictimName = crime.VictimName,
                CrimeType = crime.CrimeType,
                Notes = crime.Notes
            };
        }
    }
}
