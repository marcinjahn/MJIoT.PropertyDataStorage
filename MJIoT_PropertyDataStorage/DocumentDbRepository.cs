using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;

using ExtensionMethods;
using MJIoT.Models;

namespace MJIoT.Storage.PropertyValues
{
    public class DocumentDbRepository : IPropertyValuesStorage
    {
        private DocumentClient _client;
        private const string _endpoint = "https://mjiot.documents.azure.com:443/";
        private const string _primaryKey = "RzqbIUJO0qFiWSl6gMXvfmAeGZweUCpdK0OL9cP7aWolyWxvBaukRZTayWBdsqZl6kAfqbF4OR8kmfvjsN4mCg==";
        private const string _dataBase = "MJIoTCosmosDB";
        private const string _collection = "DevicesPropertiesData";

        public DocumentDbRepository()
        {
            _client = new DocumentClient(new Uri(_endpoint), _primaryKey);
        }

        public async Task<string> GetPropertyValueAsync(int deviceId, string propertyName)
        {
            var result = await GetPropertyDocumentAsync(deviceId, propertyName);
            return result.PropertyValue;
        }

        public async Task SetPropertyValueAsync(int deviceId, string propertyName, string propertyValue)
        {
            var document = await GetPropertyDocumentAsync(deviceId, propertyName);
            document.PropertyValue = propertyValue;

            await _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_dataBase, _collection, document.Id), document);
        }

        private async Task<PropertyDataMessage> GetPropertyDocumentAsync(int deviceId, string propertyName)
        {
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = 1 };
            var result = await _client.CreateDocumentQuery<PropertyDataMessage>(
                    UriFactory.CreateDocumentCollectionUri(_dataBase, _collection), queryOptions)
                .Where(d => d.DeviceId == deviceId && d.PropertyName == propertyName)
                .OrderByDescending(n => n.Timestamp)
                .ToListAsync();

            return result.FirstOrDefault();
        }

        private PropertyDataMessage GetPropertyDocument(int deviceId, string propertyName)
        {
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = 1 };
            var result = _client.CreateDocumentQuery<PropertyDataMessage>(
                    UriFactory.CreateDocumentCollectionUri(_dataBase, _collection), queryOptions)
                .Where(d => d.DeviceId == deviceId && d.PropertyName == propertyName)
                .OrderByDescending(n => n.Timestamp)
                .ToList();

            return result.FirstOrDefault();
        }
    }
}
