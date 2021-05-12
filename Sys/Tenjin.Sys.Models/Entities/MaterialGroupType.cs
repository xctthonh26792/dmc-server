using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Models.Entities;

namespace Tenjin.Sys.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class MaterialGroupType : BaseEntity
    {
        public string MaterialGroupCode { get; set; }

        public string MaterialSubGroupCode { get; set; }
    }
}
