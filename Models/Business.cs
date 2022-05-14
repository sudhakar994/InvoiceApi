using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InvoiceApi.Models
{
    [DataContract]
    public class Business
    {
        [DataMember]
        public Guid BusinessId { get; set; }
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public string BusinessName { get; set; }
        [DataMember]
        public string  BusinessEmail { get; set; }
        [DataMember]
        public string BusinessPhone { get; set; }
        [DataMember]
        public string PhoneCode { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
        [DataMember]
        public string State { get; set; }
    }
}
