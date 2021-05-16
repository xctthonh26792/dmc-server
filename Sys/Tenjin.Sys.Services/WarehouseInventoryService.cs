using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tenjin.Models.Cores;
using Tenjin.Reflections;
using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Services
{
    public class WarehouseInventoryService : BaseService<WarehouseInventory, WarehouseInventoryView>, IWarehouseInventoryService
    {
        private readonly ISysContext _context;
        public WarehouseInventoryService(ISysContext context) : base(context.WarehouseInventoryRepository)
        {
            _context = context;
        }

        public async Task<FetchResult<WarehouseInventoryView>> GetPageByWarehouseCode(string code, int page, int quantity = 10, SortDefinition<WarehouseInventory> sort = null, ProjectionDefinition<WarehouseInventory> projection = null)
        {
            Expression<Func<WarehouseInventory, bool>> filter = x => x.WarehouseCode == code && x.Inventory > 0;
            var value =  await GetPageByExpression(filter, page, quantity, sort, projection);
            return value;
        }

        protected override IAggregateFluent<WarehouseInventoryView> ConvertToViewAggreagate(IAggregateFluent<WarehouseInventory> mappings, IExpressionContext<WarehouseInventory, WarehouseInventoryView> context)
        {
            var unwind = new AggregateUnwindOptions<BsonDocument> { PreserveNullAndEmptyArrays = true };
            return mappings.Lookup("warehouse", "warehouse_code", "code", "warehouse").Unwind("warehouse", unwind)
                .Lookup("material", "material_code", "code", "material").Unwind("material", unwind)
                .Lookup("material_group", "material_group_code", "code", "material_group").Unwind("material_group", unwind)
                .Lookup("material_subgroup", "material_sub_group_code", "code", "material_sub_group").Unwind("material_sub_group", unwind)
                .Lookup("material_group_type", "material_group_type_code", "code", "material_group_type").Unwind("material_group_type", unwind)
                .Lookup("unit", "unit_code", "code", "unit").Unwind("unit", unwind)
                .As<WarehouseInventoryView>().Match(context.GetPostExpression());
        }
    }
}
