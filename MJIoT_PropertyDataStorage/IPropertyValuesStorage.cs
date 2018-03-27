using System.Threading.Tasks;

namespace MJIoT.Storage.PropertyValues
{
    public interface IPropertyValuesStorage
    {
        Task<string> GetPropertyValueAsync(int deviceId, string propertyName);
        Task SetPropertyValueAsync(int deviceId, string propertyName, string propertyValue);
    }
}