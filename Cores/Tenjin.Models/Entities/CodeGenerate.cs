using MongoDB.Bson.Serialization.Attributes;

namespace Tenjin.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class CodeGenerate : BaseEntity
    {
        public int Count { get; set; }
    }
}
