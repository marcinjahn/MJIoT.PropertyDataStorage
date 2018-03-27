using System;
using Newtonsoft.Json;

namespace MJIoT.Models
{
    public class PropertyDataMessage
    {
        public PropertyDataMessage()
        {

        }

        public PropertyDataMessage(string message)
        {
            var msg = JsonConvert.DeserializeObject<PropertyDataMessage>(message as string);
            DeviceId = msg.DeviceId;
            PropertyName = msg.PropertyName;
            PropertyValue = msg.PropertyValue;
            Timestamp = msg.Timestamp ?? DateTime.Now.ToString();
        }

        public PropertyDataMessage(dynamic data)
        {
            DeviceId = data.DeviceId;
            PropertyName = data.PropertyName;
            PropertyValue = data.Value;
            Timestamp = data.Timestamp ?? DateTime.Now.ToString();
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public int DeviceId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public string Timestamp { get; set; }
    }
}
