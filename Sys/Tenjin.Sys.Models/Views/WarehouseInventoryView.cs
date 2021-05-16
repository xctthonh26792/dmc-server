using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Sys.Models.Entities;

namespace Tenjin.Sys.Models.Views
{
    [BsonIgnoreExtraElements]
    public class WarehouseInventoryView : WarehouseInventory
    {

        public string FullCode
        {
            get
            {
                return $"VT{MaterialGroup?.DefCode}-{MaterialSubGroup?.DefCode}-{MaterialGroupType?.DefCode}-{Material?.DefCode}";
            }
        }

        public Warehouse Warehouse { get; set; }


        public Unit Unit { get; set; }

        public Material Material { get; set; }

        public MaterialGroup MaterialGroup { get; set; }

        public MaterialSubgroup MaterialSubGroup { get; set; }

        public MaterialGroupType MaterialGroupType { get; set; }
    }
}
