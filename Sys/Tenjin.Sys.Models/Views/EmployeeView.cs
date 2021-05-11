using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Sys.Models.Entities;

namespace Tenjin.Sys.Models.Views
{
    [BsonIgnoreExtraElements]
    public class EmployeeView : Employee
    {
        public bool HasUser { get; set; }
        public int Permission { get; set; }
        public string Username { get; set; }
        public Department Department { get; set; }
        public JobTitle JobTitle { get; set; }
        public JobPosition JobPosition { get; set; }
    }
}
