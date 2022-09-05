using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InvoiceApi.Models
{
    [DataContract]
    public class Clients
    {
        [DataMember]
        public Guid ClientId { get; set; }
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string ClientEmail { get; set; }
        [DataMember]
        public string ClientPhone { get; set; }
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
        [DataMember]
        public string CountryId { get; set; }
        [DataMember]
        public string FormattedAddress { get; set; }
    }
}
