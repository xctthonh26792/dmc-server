using MongoDB.Driver;
using Tenjin.Reflections;
using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Services
{
    public class DepartmentService : BaseService<Department, DepartmentView>, IDepartmentService
    {
        public DepartmentService(ISysContext context) : base(context.DepartmentRepository)
        {
        }

        protected override IAggregateFluent<DepartmentView> ConvertToViewAggreagate(IAggregateFluent<Department> mappings, IExpressionContext<Department, DepartmentView> context)
        {
            var unwind = new AggregateUnwindOptions<DepartmentView> { PreserveNullAndEmptyArrays = true };
            return mappings.Lookup("department", "parent_code", "code", "parent")
                .Unwind("parent", unwind)
                .As<DepartmentView>().Match(context.GetPostExpression());
        }

    }
}
