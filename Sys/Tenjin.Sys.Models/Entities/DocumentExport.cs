using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using Tenjin.Models.Entities;
using Tenjin.Sys.Models.Cores;
using Tenjin.Sys.Models.Enums;

namespace Tenjin.Sys.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class DocumentExport : BaseEntity
    {
        public string DocumentDate { get; set; }

        public bool IsDone { get; set; }

        public DocumentType Type { get; set; }

        public string WarehouseFromCode { get; set; }
        public string WarehouseToCode { get; set; }

        public string CustomerCode { get; set; }

        public string SupplierCode { get; set; }

        public List<MaterialInfo> Infos { get; set; }
    }
}
