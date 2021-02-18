using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BandApi.Data{
    public class bandcreatedtos{
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public DateTime Founded { get; set; }
        [Required]
        [MaxLength(50)]
        public string MainGenre { get; set; }
        public ICollection<AlbumnForcreatingDtos> albumns { get; set; }=new List<AlbumnForcreatingDtos>();
 

    }
}