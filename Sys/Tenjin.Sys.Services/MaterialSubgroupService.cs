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
    public class MaterialSubgroupService : BaseService<MaterialSubgroup, MaterialSubgroupView>, IMaterialSubgroupService
    {
        private readonly ISysContext _context;
        private const string TAG = "PNVT";
        public MaterialSubgroupService(ISysContext context) : base(context.MaterialSubgroupRepository)
        {
            _context = context;
        }

        protected override async Task InitializeInsertModel(MaterialSubgroup entity)
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

        protected override IAggregateFluent<MaterialSubgroupView> ConvertToViewAggreagate(IAggregateFluent<MaterialSubgroup> mappings, IExpressionContext<MaterialSubgroup, MaterialSubgroupView> context)
        {
            var unwind = new AggregateUnwindOptions<MaterialSubgroupView> { PreserveNullAndEmptyArrays = true };
            return mappings.Lookup("material_group", "material_group_code", "code", "material_group")
                .Unwind("material_group", unwind).As<MaterialSubgroupView>().Match(context.GetPostExpression());
        }

    }
}