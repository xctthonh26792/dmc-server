using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Models.Entities;

namespace Tenjin.Sys.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class Product : BaseEntity
    {
        public string SortName { get; set; }

        public string ProductGroupCode { get; set; }

        public string ProductSubgroupCode { get; set; }

        public string ShortDescription { get; set; }

        public string Producer { get; set; }
    }
}
