using MongoDB.Bson;
using MongoDB.Driver;
using Tenjin.Reflections;
using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Services
{
    public class MaterialGroupTypeService : BaseService<MaterialGroupType, MaterialGroupTypeView>, IMaterialGroupTypeService
    {
        private readonly ISysContext _context;
        public MaterialGroupTypeService(ISysContext context) : base(context.MaterialGroupTypeRepository)
        {
            _context = context;
        }

        protected override IAggregateFluent<MaterialGroupTypeView> ConvertToViewAggreagate(IAggregateFluent<MaterialGroupType> mappings, IExpressionContext<MaterialGroupType, MaterialGroupTypeView> context)
        {
            var unwind = new AggregateUnwindOptions<BsonDocument> { PreserveNullAndEmptyArrays = true };
            return mappings.Lookup("material_group", "material_group_code", "code", "material_group").Unwind("material_group")
                .Lookup("material_subgroup", "material_subgroup_code", "code", "material_subgroup").Unwind("material_subgroup")
                .As<MaterialGroupTypeView>().Match(context.GetPostExpression());
        }
    }
}
