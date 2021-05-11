using MongoDB.Bson.Serialization.Attributes;
using System;
using Tenjin.Sys.Models.Entities;

namespace Tenjin.Sys.Models.Cores
{
    [BsonIgnoreExtraElements]
    public class MaterialForInventory
    {
        public string Code { get; set; }
        public string DefCode { get; set; }
        public string Name { get; set; }
        public string SortName { get; set; }
        public string ShortDescription { get; set; }
        public string ValueToSearch { get; set; }
        public string MaterialGroupCode { get; set; }
        public string MaterialSubgroupCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Producer { get; set; }
        public bool IsCodeFixed { get; set; }
        public string FullCode
        {
            get
            {
                return $"VT{MaterialGroup?.DefCode}-{MaterialSubgroup?.DefCode}-{DefCode}";
            }
        }
        public MaterialGroup MaterialGroup { get; set; }
        public MaterialSubgroup MaterialSubgroup { get; set; }
        public int Quantity { get; set; }
        public long LastPrice { get; set; }
    }
}