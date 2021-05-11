using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Models.Entities;

namespace Tenjin.Sys.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class MaterialSubgroup : BaseEntity
    {
        public string MaterialGroupCode { get; set; }
    }
}
