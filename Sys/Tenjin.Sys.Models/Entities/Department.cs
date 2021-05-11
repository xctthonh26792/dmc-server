using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Models.Entities;

namespace Tenjin.Sys.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class Department : BaseEntity
    {
        public string ParentCode { get; set; }
    }
}
