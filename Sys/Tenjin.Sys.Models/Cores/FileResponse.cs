using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Tenjin.Sys.Models.Cores
{
    [BsonIgnoreExtraElements]
    public class FileResponse
    {
        public string Hash { get; set; }
        public string Name { get; set; }
        public string Relative { get; set; }
        public string UploadedDate { get; set; }
    }
}
