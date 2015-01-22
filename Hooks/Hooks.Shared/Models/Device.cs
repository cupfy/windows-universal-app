using System.Runtime.Serialization;

namespace Hooks.Models
{
    [DataContract]
    public class Device : BaseModel
    {
        public enum DeviceOSType { Android, iOS, WindowsPhone };

        [DataMember(Name = "_id")]
        public string ID { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "model")]
        public string Model { get; set; }

        [DataMember(Name = "type")]
        public DeviceOSType OS { get; set; }

        [DataMember(Name = "deviceId")]
        public string DeviceID { get; set; }

        [DataMember(Name = "pushId")]
        public string PushID { get; set; }
    }
}
