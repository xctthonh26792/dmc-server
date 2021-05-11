using MongoDB.Driver;
using Tenjin.Reflections;
using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Services
{
    public class ProductSubgroupService : BaseService<ProductSubgroup, ProductSubgroupView>, IProductSubgroupService
    {
        private readonly ISysContext _context;
        public ProductSubgroupService(ISysContext context) : base(context.ProductSubgroupRepository)
        {
            _context = context;
        }

        protected override IAggregateFluent<ProductSubgroupView> ConvertToViewAggreagate(IAggregateFluent<ProductSubgroup> mappings, IExpressionContext<ProductSubgroup, ProductSubgroupView> context)
        {
            var unwind = new AggregateUnwindOptions<ProductSubgroupView> { PreserveNullAndEmptyArrays = true };
            return mappings.Lookup("product_group", "product_group_code", "code", "ProductGroup")
                .Unwind("ProductGroup", unwind)
                .As<ProductSubgroupView>().Match(context.GetPostExpression());
        }

    }
}