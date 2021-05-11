using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Sys.Models.Entities;

namespace Tenjin.Sys.Models.Cores
{
    [BsonIgnoreExtraElements]
    public class MaterialForDelivery
    {
        public string Code { get; set; }
        public string DefCode { get; set; }
        public string ReceivingCode { get; set; }
        public long Inventory { get; set; }
        public long Price { get; set; }
        public string FullCode
        {
            get
            {
                return $"VT{MaterialGroup?.DefCode}-{MaterialSubgroup?.DefCode}-{DefCode}";
            }
        }
        public string Name { get; set; }
        public string SortName { get; set; }
        public string ValueToSearch { get; set; }
        public string Producer { get; set; }
        public string MaterialGroupCode { get; set; }
        public string MaterialSubgroupCode { get; set; }
        public Receiving Receiving { get; set; }
        public MaterialGroup MaterialGroup { get; set; }
        public MaterialSubgroup MaterialSubgroup { get; set; }
    }
}
