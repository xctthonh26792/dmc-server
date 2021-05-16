using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Models.Entities;

namespace Tenjin.Sys.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class Material : BaseEntity
    {
        public string SortName { get; set; }

        public string MaterialGroupCode { get; set; }

        public string MaterialSubGroupCode { get; set; }

        public string MaterialGroupTypeCode { get; set; }

        public string ShortDescription { get; set; }

        public string Producer { get; set; }

        public string UnitCode { get; set; }

        public bool IsCodeFixed { get; set; }

        public string SerialNumber { get; set; }

        public string Specification { get; set; }
    }
}
