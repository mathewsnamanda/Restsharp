using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using BandApi.Data;
using BandApi.Models;

namespace BandApi.Services{
    public class PropertyMappingService : IPropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> _bandPropertyMapping =
     new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
        {
             {"Id", new PropertyMappingValue(new List<string>(){"Id"})},
             {"Name", new PropertyMappingValue(new List<string>(){"Name"})},
             {"MainGenre", new PropertyMappingValue(new List<string>(){"MainGenre"})},
            {"foundYearsAgo", new PropertyMappingValue(new List<string>(){"Founded"},true)}

      };
        private IList<IPropertyMappingMarker> _propertyMappings = new List<IPropertyMappingMarker>();
        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<banddtos, band>(_bandPropertyMapping));
        }
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMapping = _propertyMappings.
                                                  OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
                return matchingMapping.First().MappingDictionary;

            throw new Exception("No Mapping was found");
        }
        public bool ValidMappingExist<TSource,TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();
            if (string.IsNullOrWhiteSpace(fields))
                return true;
            var fieldsAfterSplit = fields.Split(",");
            foreach(var field in fieldsAfterSplit)
            {
                var trimmedField = field.Trim();
                var indexOfSpace = trimmedField.IndexOf(" ");
                var propertyName = indexOfSpace == -1 ? trimmedField :
                    trimmedField.Remove(indexOfSpace);
                if (!propertyMapping.ContainsKey(propertyName))
                    return false;
            }
            return true;
        }

    }
}