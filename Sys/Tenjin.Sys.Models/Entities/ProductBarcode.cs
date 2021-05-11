using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Models.Entities;

namespace Tenjin.Sys.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class ProductBarcode : BaseEntity
    {
        public string ProductCode { get; set; }
        public string OrderCode { get; set; }
    }
}
