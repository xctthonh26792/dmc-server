using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Tenjin.Sys.Models.Cores
{
    [BsonIgnoreExtraElements]
    public class MaterialWarehouseReceivingInfo
    {
        public string Code { get; set; }
        public Dictionary<string, object> Store { get; set; }
        public long Price { get; set; }
        public int Quantity { get; set; }
    }
}
