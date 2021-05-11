using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Sys.Models.Entities;

namespace Tenjin.Sys.Models.Views
{
    [BsonIgnoreExtraElements]
    public class ProductView : Product
    {
        public string FullCode
        {
            get
            {
                return $"TP{ProductGroup?.DefCode}-{ProductSubgroup?.DefCode}-{DefCode}";
            }
        }

        public ProductGroup ProductGroup { get; set; }

        public ProductSubgroup ProductSubgroup { get; set; }

    }
}
