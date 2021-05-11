using MongoDB.Bson.Serialization.Attributes;

namespace Tenjin.Sys.Models.Cores
{
    [BsonIgnoreExtraElements]
    public class IdentityCard
    {
        public string CardNumber { get; set; }
        public string IssuedDate { get; set; }
        public string IssuedBy { get; set; }
    }
}
