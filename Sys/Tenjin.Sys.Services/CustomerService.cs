using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;
using MongoDB.Driver;
using Tenjin.Reflections;

namespace Tenjin.Sys.Services
{
    public class CustomerService : BaseService<Customer, CustomerView>, ICustomerService
    {
        public CustomerService(ISysContext context) : base(context.CustomerRepository)
        {
        }

        protected override IAggregateFluent<CustomerView> ConvertToViewAggreagate(IAggregateFluent<Customer> mappings, IExpressionContext<Customer, CustomerView> context)
        {
            var unwind = new AggregateUnwindOptions<CustomerView> { PreserveNullAndEmptyArrays = true };
            return mappings.Lookup("employee", "employee_code", "code", "employee").Unwind("employee", unwind)
                .As<CustomerView>().Match(context.GetPostExpression());
        }
    }
}
