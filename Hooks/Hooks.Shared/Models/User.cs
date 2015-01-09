using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Hooks.Models
{
    [DataContract]
    public class User : BaseModel
    {
        [DataMember(Name = "_id")]
        public String ID { get; private set; }

        [DataMember(Name = "apiSecret")]
        public String ApiSecret { get; set; }

        [DataMember(Name = "email")]
        public String Email { get; set; }

        [DataMember(Name = "namespace")]
        public List<String> Namespaces { get; set; }
    }
}
