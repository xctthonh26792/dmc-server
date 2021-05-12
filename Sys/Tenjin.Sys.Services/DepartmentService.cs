using MongoDB.Driver;
using System;
using System.Threading.Tasks;
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
        private readonly ISysContext _context;
        private const string TAG = "PB";
        public DepartmentService(ISysContext context) : base(context.DepartmentRepository)
        {
            _context = context;
        }

        protected override IAggregateFluent<DepartmentView> ConvertToViewAggreagate(IAggregateFluent<Department> mappings, IExpressionContext<Department, DepartmentView> context)
        {
            var unwind = new AggregateUnwindOptions<DepartmentView> { PreserveNullAndEmptyArrays = true };
            return mappings.Lookup("department", "parent_code", "code", "parent")
                .Unwind("parent", unwind)
                .As<DepartmentView>().Match(context.GetPostExpression());
        }

        protected override async Task InitializeInsertModel(Department model)
        {
            model.Code = await GenerateCode();
            model.DefCode = string.IsNullOrEmpty(model.DefCode) ? model.Code : model.DefCode;
            model.IsPublished = true;
            await base.InitializeInsertModel(model);
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

    }
}
