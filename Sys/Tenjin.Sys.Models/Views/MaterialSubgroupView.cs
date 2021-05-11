using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Sys.Models.Entities;

namespace Tenjin.Sys.Models.Views
{
    [BsonIgnoreExtraElements]
    public class MaterialSubgroupView : MaterialSubgroup
    {
        public string FullCode
        {
            get
            {
                return $"{MaterialGroup?.DefCode}-{DefCode}";
            }
        }

        public MaterialGroup MaterialGroup { get; set; }
    }
}
