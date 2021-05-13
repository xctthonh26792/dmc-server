using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class DocumentExportService : BaseService<DocumentExport, DocumentExportView>, IDocumentExportService
    {
        private readonly ISysContext _context;
        public DocumentExportService(ISysContext context) : base(context.DocumentExportRepository)
        {
            _context = context;
        }

        public override async Task Add(DocumentExport entity)
        {
            await base.Add(entity);
            if (TenjinUtils.IsArrayNotEmpty(entity.Infos))
            {
                foreach (var info in entity.Infos)
                {
                    var filter = Builders<WarehouseInventory>.Filter.And(
                        Builders<WarehouseInventory>.Filter.Eq(x => x.MaterialCode, info.Code),
                        Builders<WarehouseInventory>.Filter.Eq(x => x.WarehouseCode, entity.WarehouseCode),
                        Builders<WarehouseInventory>.Filter.Eq(x => x.UnitCode, info.UnitCode));
                    var updater = Builders<WarehouseInventory>.Update
                        .Inc(x => x.Quantity, -info.Quantity);
                    await _context.WarehouseInventoryRepository.UpsertOne(filter, updater);
                }
            }
        }

        protected override IAggregateFluent<DocumentExportView> ConvertToViewAggreagate(IAggregateFluent<DocumentExport> mappings, IExpressionContext<DocumentExport, DocumentExportView> context)
        {
            var unwind = new AggregateUnwindOptions<BsonDocument> { PreserveNullAndEmptyArrays = true };
            return mappings.Lookup("warehouse", "warehouse_code", "code", "warehouse").Unwind("warehouse")
                .Lookup("customer", "customer_code", "code", "customer").Unwind("customer")
                .Lookup("supplier", "supplier_code", "code", "supplier").Unwind("supplier")
                .As<DocumentExportView>().Match(context.GetPostExpression());
        }
    }
}
