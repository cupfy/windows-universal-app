using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Hooks.Models
{
    [DataContract]
    public class Hook : BaseModel
    {
        [DataMember(Name = "_id")]
        public string ID { get; private set; }

        [DataMember(Name = "namespace")]
        public string Namespace { get; set; }

        [DataMember(Name = "approved")]
        public bool Approved { get; set; }

        [DataMember(Name = "removed")]
        public bool Removed { get; set; }

        public bool Enabled { get { return Approved && !Removed; } set { Removed = !value; } }
    }
    
    public class HookList : ObservableCollection<Hook> { }
}
