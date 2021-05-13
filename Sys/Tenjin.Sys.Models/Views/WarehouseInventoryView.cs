using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Sys.Models.Entities;

namespace Tenjin.Sys.Models.Views
{
    [BsonIgnoreExtraElements]
    public class WarehouseInventoryView : WarehouseInventory
    {
        public Warehouse Warehouse { get; set; }


        public Unit Unit { get; set; }

        public Material Material { get; set; }

        public MaterialGroup MaterialGroup { get; set; }

        public MaterialSubgroup MaterialSubgroup { get; set; }

        public MaterialGroupType MaterialGroupType { get; set; }
    }
}
