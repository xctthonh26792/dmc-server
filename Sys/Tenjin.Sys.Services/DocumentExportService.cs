using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tenjin.Contracts.Interfaces;
using Tenjin.Helpers;
using Tenjin.Models.Entities;
using Tenjin.Reflections;
using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Cores;
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
            entity.IsPublished = true;
            await base.Add(entity);
            if (TenjinUtils.IsArrayNotEmpty(entity.Infos))
            {
                foreach (var info in entity.Infos)
                {
                    var filter = Builders<WarehouseInventory>.Filter.And(
                        Builders<WarehouseInventory>.Filter.Eq(x => x.MaterialCode, info.Code),
                        Builders<WarehouseInventory>.Filter.Eq(x => x.WarehouseCode, entity.WarehouseFromCode),
                        Builders<WarehouseInventory>.Filter.Eq(x => x.UnitCode, info.UnitCode));
                    var updater = Builders<WarehouseInventory>.Update
                        .Inc(x => x.Inventory, -info.Quantity);
                    await _context.WarehouseInventoryRepository.UpsertOne(filter, updater);
                }
            }
        }

        public async Task<DocumentExportResolve> DocumentExportResolve()
        {
            return new DocumentExportResolve
            {
                Customers = await Resolve(_context.CustomerRepository, x => x.IsPublished == true),
                Suppliers = await Resolve(_context.SupplierRepository, x => x.IsPublished == true),
                Warehouses = await Resolve(_context.WarehouseRepository, x => x.IsPublished == true)
            };
            static async Task<IEnumerable<T>> Resolve<T>(IRepository<T> repository, Expression<Func<T, bool>> filter)
                where T : BaseEntity
            {
                return await repository.GetByExpression(filter);
            }
        }

        protected override IAggregateFluent<DocumentExportView> ConvertToViewAggreagate(IAggregateFluent<DocumentExport> mappings, IExpressionContext<DocumentExport, DocumentExportView> context)
        {
            var unwind = new AggregateUnwindOptions<BsonDocument> { PreserveNullAndEmptyArrays = true };
            return mappings
                .Lookup("warehouse", "warehouse_from_code", "code", "warehouse_from").Unwind("warehouse_from", unwind)
                .Lookup("warehouse", "warehouse_to_code", "code", "warehouse_to").Unwind("warehouse_to", unwind)
                .Lookup("customer", "customer_code", "code", "customer").Unwind("customer", unwind)
                .Lookup("supplier", "supplier_code", "code", "supplier").Unwind("supplier", unwind)
                .As<DocumentExportView>().Match(context.GetPostExpression());
        }
    }
}
