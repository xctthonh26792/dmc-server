using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Sys.Models.Entities;

namespace Tenjin.Sys.Models.Views
{
    [BsonIgnoreExtraElements]
    public class MaterialReceivingVoucherView : MaterialReceivingVoucher
    {
        public Supplier Supplier { get; set; }
    }
}
