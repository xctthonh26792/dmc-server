using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Tenjin.Sys.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class Receiving
    {
        public string Code { get; set; }
        public string DefCode { get; set; }
        public string DocumentDate { get; set; }
    }
}
