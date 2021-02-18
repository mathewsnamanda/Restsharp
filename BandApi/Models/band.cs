using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BandApi.Models
{
    public class band
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public DateTime Founded { get; set; }
        [Required]
        [MaxLength(50)]
        public string MainGenre { get; set; }
        public ICollection<album> albumns { get; set; } = new List<album>();
    }
}
