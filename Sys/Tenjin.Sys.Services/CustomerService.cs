using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;
using MongoDB.Driver;
using Tenjin.Reflections;
using System.Threading.Tasks;
using System;

namespace Tenjin.Sys.Services
{
    public class CustomerService : BaseService<Customer, CustomerView>, ICustomerService
    {
        private readonly ISysContext _context;
        private const string TAG = "KH";
        public CustomerService(ISysContext context) : base(context.CustomerRepository)
        {
            _context = context;
        }

        protected override async Task InitializeInsertModel(Customer entity)
        {
            await base.InitializeInsertModel(entity);
            entity.Code = await GenerateCode();
            entity.IsPublished = true;
            entity.DefCode = string.IsNullOrEmpty(entity.DefCode) ? entity.Code : entity.DefCode;
        }

        public async Task<string> GenerateCode()
        {
            var today = DateTime.Now.ToString("yyMM");
            var code = $"{TAG}-{today}";
            var filter = Builders<CodeGenerate>.Filter.Where(x => x.Code == code);
            var updater = Builders<CodeGenerate>.Update
                .SetOnInsert(x => x.Code, code)
                .SetOnInsert(x => x.CreatedDate, DateTime.Now)
                .Set(x => x.LastModified, DateTime.Now)
                .Set(x => x.IsPublished, true)
                .Inc(x => x.Count, 1);
            var options = new FindOneAndUpdateOptions<CodeGenerate, CodeGenerate>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };
            var model = await _context.CodeGenerateRepository.GetCollection().FindOneAndUpdateAsync(filter, updater, options);
            return $"{TAG}{today}{model.Count:D3}";
        }

        protected override IAggregateFluent<CustomerView> ConvertToViewAggreagate(IAggregateFluent<Customer> mappings, IExpressionContext<Customer, CustomerView> context)
        {
            var unwind = new AggregateUnwindOptions<CustomerView> { PreserveNullAndEmptyArrays = true };
            return mappings.Lookup("employee", "employee_code", "code", "employee").Unwind("employee", unwind)
                .As<CustomerView>().Match(context.GetPostExpression());
        }
    }
}
