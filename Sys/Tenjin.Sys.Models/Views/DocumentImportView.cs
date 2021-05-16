using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Enums;

namespace Tenjin.Sys.Models.Views
{
    public class DocumentImportView : DocumentImport
    {
        public string Partner { get
            {
                if (Type == DocumentType.Customer)
                {
                    return $"{Customer?.Name}";
                }
                else if (Type == DocumentType.Supplier)
                {
                    return $"{Supplier?.Name}";
                }
                else {
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
