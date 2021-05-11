using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Services
{
    public class UnitService : BaseService<Unit>, IUnitService
    {
        public UnitService(ISysContext context) : base(context.UnitRepository)
        {
        }
    }
}
