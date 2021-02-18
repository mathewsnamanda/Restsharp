using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BandApi.helpers{
    public class arraymodelbinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
           if(bindingContext.ModelMetadata.IsEnumerableType)
           {
               bindingContext.Result=ModelBindingResult.Failed();
           }
           var value=bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();
           if(string.IsNullOrWhiteSpace(value))
           {
                 bindingContext.Result=ModelBindingResult.Success(null);     
                 return Task.CompletedTask;
           }
           var elementtype=bindingContext.ModelType.GetType().GenericTypeArguments[0];
           var converter= TypeDescriptor.GetConverter(elementtype);
           var values=value.Split(new[] {","},System.StringSplitOptions.RemoveEmptyEntries)
                           .Select(x=>converter.ConvertFromString(x.Trim())).ToArray();

           var typeValues = Array.CreateInstance(elementtype, values.Length);
           values.CopyTo(typeValues,0);
           bindingContext.Model=typeValues;
           bindingContext.Result=ModelBindingResult.Success(bindingContext.Model);
           return Task.CompletedTask;
        }
    }
}