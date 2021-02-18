using System.ComponentModel.DataAnnotations;
using BandApi.ValidationAttributes;

namespace BandApi.Data{
      [TitleDescrption(ErrorMessage="title must be different from description")] 
    public abstract class albumnmanipulationdtos{
        [Required(ErrorMessage="Title needs to be filled")]
        [MaxLength(200,ErrorMessage="title needs to be upto to 200 characters")]

        public string Title { get; set; }
        [MaxLength(400,ErrorMessage="Description needs to be upto to 400 characters")]
        public virtual string Description { get; set; }
    }
    }
