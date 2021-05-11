using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tenjin.Helpers;
using Tenjin.Reflections;
using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Cores;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Services
{
    public class EmployeeService : BaseService<Employee, EmployeeView>, IEmployeeService
    {
        private readonly ISysContext _context;
        private const string TAG = "NV";
        public EmployeeService(ISysContext context) : base(context.EmployeeRepository)
        {
            _context = context;
        }

        

        protected override IAggregateFluent<EmployeeView> ConvertToViewAggreagate(IAggregateFluent<Employee> mappings, IExpressionContext<Employee, EmployeeView> context)
        {
            var afState = @"{
                                ""$addFields"": {
                                    ""has_user"": { ""$cond"": [{ ""$ne"": [ ""$user"", undefined ] }, true, false ] },
                                    ""username"": ""$user.username"",
                                    ""uermission"": ""$user.uermission""
                                }
                            }";
            var pjState = @"{
                                ""User"": 0
                            }";
            var unwind = new AggregateUnwindOptions<BsonDocument> { PreserveNullAndEmptyArrays = true };
            // TODO: Mapping them JobPosition + Department + JobTitle
            return mappings
                .Lookup("user", "user_code", "code", "user").Unwind("user", unwind)
                .Lookup("job_position", "job_position_code", "code", "job_position").Unwind("job_position", unwind)
                .Lookup("department", "job_position.department_code", "code", "department").Unwind("department", unwind)
                .Lookup("job_title", "job_position.job_title_code", "code", "job_title").Unwind("job_title", unwind)
                .AppendStage<BsonDocument>(BsonDocument.Parse(afState))
                .Project(BsonDocument.Parse(pjState))
                .As<EmployeeView>().Match(context.GetPostExpression());
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

        protected override async Task InitializeInsertModel(Employee entity)
        {
            await base.InitializeInsertModel(entity);
            if (entity != null)
            {
                var parts = entity.Name?.Split(' ');
                entity.FirstName = parts.LastOrDefault()?.ViToEn() ?? string.Empty;
            }
            entity.DefCode = string.IsNullOrEmpty(entity.DefCode) ? await GenerateCode() : entity.DefCode;
            entity.IsPublished = true;
        }

        protected override async Task InitializeReplaceModel(Employee entity)
        {
            await base.InitializeReplaceModel(entity);
            if (entity != null)
            {
                var parts = entity.Name?.Split(' ');
                entity.FirstName = parts.LastOrDefault()?.ViToEn() ?? string.Empty;
            }
            entity.DefCode = string.IsNullOrEmpty(entity.DefCode) ? await GenerateCode() : entity.DefCode;
        }

    }
}
