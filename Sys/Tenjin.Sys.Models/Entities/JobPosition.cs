using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Models.Attributes;
using Tenjin.Models.Entities;
using Tenjin.Models.Enums;

namespace Tenjin.Sys.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class JobPosition : BaseEntity
    {
        [BsonProperty(BsonDirection.DESC)]
        public string JobFunctions { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string JobDuties { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string JobTitleCode { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string DepartmentCode { get; set; }
    }
}
