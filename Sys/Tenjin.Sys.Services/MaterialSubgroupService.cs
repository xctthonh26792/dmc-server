using MongoDB.Driver;
using Tenjin.Reflections;
using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Services
{
    public class MaterialSubgroupService : BaseService<MaterialSubgroup, MaterialSubgroupView>, IMaterialSubgroupService
    {
        private readonly ISysContext _context;
        public MaterialSubgroupService(ISysContext context) : base(context.MaterialSubgroupRepository)
        {
            _context = context;
        }

        protected override IAggregateFluent<MaterialSubgroupView> ConvertToViewAggreagate(IAggregateFluent<MaterialSubgroup> mappings, IExpressionContext<MaterialSubgroup, MaterialSubgroupView> context)
        {
            var unwind = new AggregateUnwindOptions<MaterialSubgroupView> { PreserveNullAndEmptyArrays = true };
            return mappings.Lookup("material_group", "material_group_code", "code", "material_group")
                .Unwind("material_group", unwind).As<MaterialSubgroupView>().Match(context.GetPostExpression());
        }

    }
}