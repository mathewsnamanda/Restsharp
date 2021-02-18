using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandApi.helpers
{
    public static class FoundYearsAgo
    {
        public static int GetYearsAgo(this DateTime dateTime)
        {
            var currentdate = DateTime.Now;
            int yearsago = currentdate.Year - dateTime.Year;
            return yearsago;
        }
    }
}
