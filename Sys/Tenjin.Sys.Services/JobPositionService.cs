using Tenjin.Services;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;
using Tenjin.Sys.Contracts.Interfaces;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tenjin.Models.Enums;
using System.Linq.Expressions;
using System;
using Tenjin.Helpers;
using Tenjin.Reflections;

namespace Tenjin.Sys.Services
{
    public class JobPositionService : BaseService<JobPosition, JobPositionView>, IJobPositionService
    {
        private readonly ISysContext _context;
        private const string TAG = "VTCV";
        public JobPositionService(ISysContext context) : base(context.JobPositionRepository)
        {
            _context = context;
        }

        protected override IAggregateFluent<JobPositionView> ConvertToViewAggreagate(IAggregateFluent<JobPosition> mappings, IExpressionContext<JobPosition, JobPositionView> context)
        {
            var unwind = new AggregateUnwindOptions<DepartmentView> { PreserveNullAndEmptyArrays = true };
            return mappings
                .Lookup("job_title", "job_title_code", "code", "job_title")
                .Unwind("job_title", unwind)
                .Lookup("department", "department_code", "code", "department")
                .Unwind("department", unwind)
                .As<JobPositionView>().Match(context.GetPostExpression());
        }

        protected override async Task InitializeInsertModel(JobPosition entity)
        {
            entity.Code = await GenerateCode();
            entity.DefCode = string.IsNullOrEmpty(entity.DefCode) ? entity.Code : entity.DefCode;
            entity.IsPublished = true;
            await base.InitializeInsertModel(entity);
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
