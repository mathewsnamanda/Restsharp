using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BandApi.Data
{
    public class banddtos
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public String FoundYearsAgo { get; set; }
        public string MainGenre { get; set; }
    }
}
