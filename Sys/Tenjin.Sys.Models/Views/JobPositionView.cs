using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Sys.Models.Entities;

namespace Tenjin.Sys.Models.Views
{
    [BsonIgnoreExtraElements]
    public class JobPositionView : JobPosition
    {
        public Department Department { get; set; }
        public JobTitle JobTitle { get; set; }
    }
}
