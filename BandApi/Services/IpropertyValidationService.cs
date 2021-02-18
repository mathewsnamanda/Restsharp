namespace BandApi.Services
{
    public interface IpropertyValidationService
    {
        bool HasValidProperties<T>(string fields);
    }
}