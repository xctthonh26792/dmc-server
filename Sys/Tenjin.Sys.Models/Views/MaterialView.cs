using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Sys.Models.Entities;

namespace Tenjin.Sys.Models.Views
{
    [BsonIgnoreExtraElements]
    public class MaterialView : Material
    {
        public string FullCode
        {
            get
            {
                return $"VT{MaterialGroup?.DefCode}-{MaterialSubGroup?.DefCode}-{MaterialGroupType?.DefCode}-{DefCode}";
            }
        }

        public MaterialGroup MaterialGroup { get; set; }
        public MaterialGroupType MaterialGroupType { get; set; }

        public MaterialSubgroup MaterialSubGroup { get; set; }

        public Unit Unit { get; set; }

    }
}
