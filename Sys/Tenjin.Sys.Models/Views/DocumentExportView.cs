using MongoDB.Bson.Serialization.Attributes;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Enums;

namespace Tenjin.Sys.Models.Views
{
    [BsonIgnoreExtraElements]
    public class DocumentExportView : DocumentExport
    {
        public string Partner
        {
            get
            {
                if (Type == DocumentType.Customer)
                {
                    return $"{Customer?.Name}";
                }
                else if (Type == DocumentType.Supplier)
                {
                    return $"{Supplier?.Name}";
                }
                else
                {
                    return $"{WarehouseTo?.Name}";
                }
            }
        }

        public Warehouse WarehouseFrom { get; set; }
        public Warehouse WarehouseTo { get; set; }

        public Customer Customer { get; set; }

        public Supplier Supplier { get; set; }
    }
}
