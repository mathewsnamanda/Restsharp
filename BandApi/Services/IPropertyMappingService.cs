using System.Collections.Generic;

namespace BandApi.Services
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> 
        GetPropertyMapping<TSource, TDestination>();
        bool ValidMappingExist<TSource, TDestination>(string fields);
    }
}