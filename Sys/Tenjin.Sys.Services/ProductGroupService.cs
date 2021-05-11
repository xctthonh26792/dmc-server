using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Services
{
    public class ProductGroupService : BaseService<ProductGroup>, IProductGroupService
    {
        public ProductGroupService(ISysContext context) : base(context.ProductGroupRepository)
        {
        }
    }
}
