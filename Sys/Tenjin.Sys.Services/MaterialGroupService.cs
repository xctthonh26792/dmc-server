using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Services
{
    public class MaterialGroupService : BaseService<MaterialGroup>, IMaterialGroupService
    {
        public MaterialGroupService(ISysContext context) : base(context.MaterialGroupRepository)
        {
        }
    }
}
