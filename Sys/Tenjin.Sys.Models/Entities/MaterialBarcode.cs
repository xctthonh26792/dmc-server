using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenjin.Models.Attributes;
using Tenjin.Models.Entities;
using Tenjin.Models.Enums;

namespace Tenjin.Sys.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class MaterialBarcode : BaseEntity
    {
        [BsonProperty(BsonDirection.DESC)]
        public string MaterialCode { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string ReceiptCode { get; set; }

        [BsonProperty(BsonDirection.DESC)]
        public string Barcode { get; set; }
    }
}
