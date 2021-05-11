using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using Tenjin.Models.Entities;
using Tenjin.Sys.Models.Cores;

namespace Tenjin.Sys.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class MaterialReceivingVoucher : BaseEntity
    {
        public string DocumentDate { get; set; }
        public string SupplierCode { get; set; }
        public string Category { get; set; }
        public string DeliveryCode { get; set; }
        public IEnumerable<MaterialWarehouseReceivingInfo> Infos { get; set; }
    }
}
