using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BandApi.Models
{
    public class album
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(400)]
        public string Description { get; set; }
        [ForeignKey("BandId")]
        public band band { get; set; }
        public Guid BandId { get; set; }
    }
}
