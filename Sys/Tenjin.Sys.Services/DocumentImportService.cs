using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using Tenjin.Helpers;
using Tenjin.Reflections;
using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Services
{
    public class DocumentImportService : BaseService<DocumentImport, DocumentImportView>, IDocumentImportService
    {
        private readonly ISysContext _context;
        public DocumentImportService(ISysContext context ): base(context.DocumentImportRepository)
        {
            _context = context;
        }

        public override async Task Add(DocumentImport entity)
        {
             await base.Add(entity);
            if(TenjinUtils.IsArrayNotEmpty(entity.Infos))
            {
                foreach(var info in entity.Infos)
                {
                    var filter = Builders<WarehouseInventory>.Filter.And(
                        Builders<WarehouseInventory>.Filter.Eq(x => x.MaterialCode, info.Code),
                        Builders<WarehouseInventory>.Filter.Eq(x => x.WarehouseCode, entity.WarehouseCode),
                        Builders<WarehouseInventory>.Filter.Eq(x => x.UnitCode, info.UnitCode));
                    var updater = Builders<WarehouseInventory>.Update
                        .Inc(x => x.Quantity, info.Quantity)
                        .Set(x => x.UnitCode, info.UnitCode)
                        .Set(x => x.WarehouseCode, entity.WarehouseCode)
                        .Set(x => x.MaterialCode, info.Code)
                        .Set(x => x.MaterialGroupCode, info.MaterialGroupCode)
                        .Set(x => x.MaterialSubgroupCode, info.MaterialSubgroupCode)
                        .Set(x => x.MaterialGroupTypeCode, info.MaterialGroupTypeCode)
                        .Set(x => x.SerialNumber, info.SerialNumber)
                        .Set(x => x.Specification, info.Specification);
                    await _context.WarehouseInventoryRepository.UpsertOne(filter, updater);
                }
            }
        }

        protected override IAggregateFluent<DocumentImportView> ConvertToViewAggreagate(IAggregateFluent<DocumentImport> mappings, IExpressionContext<DocumentImport, DocumentImportView> context)
        {
            var unwind = new AggregateUnwindOptions<BsonDocument> { PreserveNullAndEmptyArrays = true };
            return mappings.Lookup("warehouse", "warehouse_code", "code", "warehouse").Unwind("warehouse")
                .Lookup("customer", "customer_code", "code", "customer").Unwind("customer")
                .Lookup("supplier", "supplier_code", "code", "supplier").Unwind("supplier")
                .As<DocumentImportView>().Match(context.GetPostExpression());
        }
    }
}
