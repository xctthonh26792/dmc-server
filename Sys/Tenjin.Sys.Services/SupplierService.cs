using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Services
{
    public class SupplierService : BaseService<Supplier>, ISupplierService
    {
        public SupplierService(ISysContext context) : base(context.SupplierRepository)
        {
        }
    }
}
