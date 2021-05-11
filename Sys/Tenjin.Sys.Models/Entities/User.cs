using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using Tenjin.Models.Entities;

namespace Tenjin.Sys.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class User : BaseEntity
    {
        public User()
        {
            ExtraProps = new Dictionary<string, string>();
        }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public int Permission { get; set; }

        public string Permissions { get; set; }

        public Dictionary<string, string> ExtraProps { get; set; }
    }
}
