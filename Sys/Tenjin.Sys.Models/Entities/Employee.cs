using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using Tenjin.Helpers;
using Tenjin.Models.Attributes;
using Tenjin.Models.Entities;
using Tenjin.Models.Enums;
using Tenjin.Sys.Models.Cores;

namespace Tenjin.Sys.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class Employee : BaseEntity
    {
        [BsonProperty(BsonDirection.DESC)]
        public string FirstName { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string DateOfBirth { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string Email { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string Phone { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string Address { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string StartDate { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string LeaveDate { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public IdentityCard IdentityCard { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string UserCode { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string JobPositionCode { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string Gender { get; set; }

        public List<FileResponse> Documents { get; set; }

        public override string ValueToSearch => $"{Name?.ToSeoUrl()} {FirstName?.ToSeoUrl()} {DefCode?.ToSeoUrl()} {Phone?.ToSeoUrl()} {Email?.ToSeoUrl()}";
    }
}
