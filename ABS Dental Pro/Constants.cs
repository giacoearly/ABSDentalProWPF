using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABS_Dental_Pro
{
    static class Constants
    {
        public const int NrPacientiDataGrid = 28;
        public static string[] ZileleSaptamanii = { "", "Lu", "Ma", "Mi", "Joi", "Vi", "Sâ", "Du" };
        public static string[] LunileAnului = { "", "Ian", "Feb", "Mar", "Apr", "Mai", "Iun", "Iul", "Aug", "Sep", "Oct", "Noi", "Dec" };
        // pentru ProgramariPacienti, ShrinkPrenume, la prescurtare, ca sa incapa in dreptunghi 
        public const int LungimeMaximaNumePrenume = 16;
        public const int OrizontDeTimpProgramariPacienti = 16;
    }
}