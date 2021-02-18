using System.ComponentModel.DataAnnotations;
using BandApi.Data;

namespace BandApi.ValidationAttributes{
    public class TitleDescrptionAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var albumn=(albumnmanipulationdtos)validationContext.ObjectInstance;
            if(albumn.Title==albumn.Description){
                return new ValidationResult("Title and Description should not be same",new []{"albumnmanipulationdtos"});
            }
            return ValidationResult.Success;
        }
    }
}