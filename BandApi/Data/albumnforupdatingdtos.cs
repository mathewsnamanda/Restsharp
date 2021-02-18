using System;
using System.ComponentModel.DataAnnotations;
using BandApi.ValidationAttributes;

namespace BandApi.Data{
       public class albumnforupdatingdtos:albumnmanipulationdtos
       {
           [Required(ErrorMessage="Description is required")]
            public override string Description { get =>base.Description; 
            set =>base.Description=value ; }
       }
}