using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Sys.Models.Entities;

namespace Tenjin.Sys.Models.Views
{
    [BsonIgnoreExtraElements]
    public class MaterialDeliveryVoucherView : MaterialDeliveryVoucher
    {
        public Customer Customer { get; set; }
        public Customer OtherCustomer { get; set; }
        public Supplier Supplier { get; set; }
    }
}
