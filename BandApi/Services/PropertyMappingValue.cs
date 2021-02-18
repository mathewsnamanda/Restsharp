using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandApi.Services{
    public class PropertyMappingValue
    {
       public IEnumerable<string> DestinationProperties { get; set; } 
       public bool Revert { get; set; }
       public PropertyMappingValue(IEnumerable<string> destinationproperties,
       bool revert=false
       )
       {
          DestinationProperties = destinationproperties??
          throw new ArgumentNullException(nameof(destinationproperties));
          Revert=revert;
       }
    }
}