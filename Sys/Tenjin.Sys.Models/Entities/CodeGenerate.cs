using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Models.Entities;

namespace Tenjin.Sys.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class CodeGenerate : BaseEntity
    {
        public int Count { get; set; }
    }
}
