using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using Tenjin.Models.Attributes;
using Tenjin.Models.Entities;
using Tenjin.Models.Enums;
using Tenjin.Sys.Models.Cores;

namespace Tenjin.Sys.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class MaterialDeliveryVoucher : BaseEntity
    {
        public string DeliveryType { get; set; }

        public string OtherType { get; set; }
        public string CustomerCode { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string OtherCustomerCode { get; set; }
        public string SupplierCode { get; set; }
        public string InvoiceCode { get; set; }
        public string Reason { get; set; }
        public string DocumentDate { get; set; }
        public IEnumerable<MaterialWarehouseDeliveryVoucherInfo> Infos { get; set; }
    }
}
