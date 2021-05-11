using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Models.Entities;

namespace Tenjin.Sys.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class Customer : BaseEntity
    {
        public string Trademark { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string TaxCode { get; set; }
        public string ContactPerson { get; set; }
        public string EmployeeCode { get; set; }
    }
}
