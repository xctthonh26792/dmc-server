using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Models.Attributes;
using Tenjin.Models.Entities;
using Tenjin.Models.Enums;

namespace Tenjin.Sys.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class Company : BaseEntity
    {
        [BsonProperty(BsonDirection.DESC)]
        public string Address { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string Email { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string Phone { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string Fax { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string TaxCode { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string Representative { get; set; }
    }
}
