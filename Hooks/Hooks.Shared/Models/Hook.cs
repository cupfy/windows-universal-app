using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Hooks.Common;

namespace Hooks.Models
{
    [DataContract]
    public class Hook
    {
        [DataMember(Name = "_id")]
        public String ID { get; set; }

        [DataMember(Name = "namespace")]
        public String Namespace { get; set; }

        [DataMember(Name = "approved")]
        public bool Approved { get; set; }

        [DataMember(Name = "removed")]
        public bool Disabled { get; set; }

        public bool Enabled { get { return !Disabled; } set { Disabled = !value; } }
    }
    
    public class HookList : ObservableCollection<Hook> { }
}
