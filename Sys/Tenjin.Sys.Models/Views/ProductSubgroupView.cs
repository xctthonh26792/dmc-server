using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Sys.Models.Entities;

namespace Tenjin.Sys.Models.Views
{
    [BsonIgnoreExtraElements]
    public class ProductSubgroupView : ProductSubgroup
    {
        public string FullCode
        {
            get
            {
                return $"{ProductGroup?.DefCode}-{DefCode}";
            }
        }

        public ProductGroup ProductGroup { get; set; }
    }
}
