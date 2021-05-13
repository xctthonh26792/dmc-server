using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Models.Entities;

namespace Tenjin.Sys.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class WarehouseInventory : BaseEntity
    {
        public string WarehouseCode { get; set; }

        public long Quantity { get; set; }

        public string UnitCode { get; set; }

        public string MaterialCode { get; set; }

        public string MaterialGroupCode { get; set; }

        public string MaterialSubgroupCode { get; set; }

        public string MaterialGroupTypeCode { get; set; }

        public string SerialNumber { get; set; }

        public string Specification { get; set; }
    }
}
