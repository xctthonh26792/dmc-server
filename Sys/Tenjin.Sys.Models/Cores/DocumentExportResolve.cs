using System.Collections.Generic;
using Tenjin.Models.Interfaces;

namespace Tenjin.Sys.Models.Cores
{
    public class DocumentExportResolve
    {
        public IEnumerable<IEntity> Warehouses { get; set; }
        public IEnumerable<IEntity> Customers { get; set; }
        public IEnumerable<IEntity> Suppliers { get; set; }
    }
}
