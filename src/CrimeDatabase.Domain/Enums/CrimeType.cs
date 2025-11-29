using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeDatabase.Domain.Enums
{
    /// <summary>
    /// This is enum represents the possible crime categories.
    /// I have kept this free of Display attributes to separate the UI concerns from domain logic.
    /// </summary>
    public enum CrimeType
    {
        Burglary,
        Robbery,
        // I have added some additional crime types below.
        Violent,
        Sexual,
        AntiSocialBehaviour,
        CriminalDamage
    }
}
