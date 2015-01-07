using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Hooks.Models
{
    [DataContract]
    public class User
    {
        [DataMember(Name = "_id")]
        public String ID { get; set; }

        [DataMember(Name = "apiSecret")]
        public String ApiSecret { get; set; }

        [DataMember(Name = "email")]
        public String Email { get; set; }

        [DataMember(Name = "namespace")]
        public List<String> Namespaces { get; set; }
    }
}
