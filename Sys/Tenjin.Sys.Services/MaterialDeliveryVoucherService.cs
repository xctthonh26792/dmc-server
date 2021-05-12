using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using Tenjin.Reflections;
using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Services
{
    public class MaterialDeliveryVoucherService : BaseService<MaterialDeliveryVoucher, MaterialDeliveryVoucherView>, IMaterialDeliveryVoucherService
    {
        private readonly ISysContext _context;
        public MaterialDeliveryVoucherService(ISysContext context) : base(context.MaterialDeliveryVoucherRepository)
        {
            _context = context;
        }


        protected override async Task InitializeInsertModel(MaterialDeliveryVoucher entity)
        {

            if (entity.DeliveryType == "other")
            {
                entity.CustomerCode = string.Empty;
                entity.InvoiceCode = string.Empty;
            }
            else if (entity.DeliveryType == "manufacturing")
            {
                entity.CustomerCode = string.Empty;
                entity.Reason = string.Empty;
            }
            else if (entity.DeliveryType == "business")
            {
                entity.InvoiceCode = string.Empty;
                entity.Reason = string.Empty;
            }
            await base.InitializeInsertModel(entity);
        }

        protected override async Task InitializeReplaceModel(MaterialDeliveryVoucher entity)
        {

            if (entity.DeliveryType == "other")
            {
                entity.CustomerCode = string.Empty;
                entity.InvoiceCode = string.Empty;
            }
            else if (entity.DeliveryType == "manufacturing")
            {
                entity.CustomerCode = string.Empty;
                entity.Reason = string.Empty;
            }
            else if (entity.DeliveryType == "business")
            {
                entity.InvoiceCode = string.Empty;
                entity.Reason = string.Empty;
            }
            await base.InitializeReplaceModel(entity);
        }

        protected override IAggregateFluent<MaterialDeliveryVoucherView> ConvertToViewAggreagate(IAggregateFluent<MaterialDeliveryVoucher> mappings, IExpressionContext<MaterialDeliveryVoucher, MaterialDeliveryVoucherView> context)
        {
            var unwind = new AggregateUnwindOptions<MaterialDeliveryVoucher> { PreserveNullAndEmptyArrays = true };
            var group = @"
                {
                    _id: {
                        _id : ""$_id"", 
                        code : ""$Code"", 
                        def_code : ""$DefCode"",
                        name : ""$name"",
                        value_to_search : ""$value_to_search"", 
                        is_published : ""$is_published"", 
                        created_date : ""$created_date"", 
                        last_modified : ""$last_modified"",
                        delivery_type: ""$delivery_type"",
                        other_type: ""$other_type"",
                        customer_code : ""$customer_code"", 
                        other_customer_code : ""$other_customer_code"", 
                        supplier_code : ""$supplier_code"", 
                        invoice_code : ""$invoice_code"", 
                        reason : ""$reason"", 
                        description: ""$description"",
                        document_date : ""$document_date""
                    },
                    infos: { ""$addToSet"": ""$infos"" }
                }";
            var projection = @"
                {
                    _id: ""$_id._id"",
                    code: ""$_id.code"",
                    def_code: ""$_id.def_code"",
                    name : ""$_id.name"",
                    delivery_type: ""$_id.delivery_type"",
                    other_type: ""$_id.other_type"",
                    description: ""$_id.description"",
                    value_to_search: ""$_id.value_to_search"",
                    is_published: ""$_id.is_published"",
                    created_date: ""$_id.created_date"",
                    last_modified: ""$_id.last_modified"",
                    customer_code : ""$_id.customer_code"", 
                    other_customer_code : ""$_id.other_customer_code"", 
                    supplier_code : ""$_id.supplier_code"", 
                    invoice_code : ""$_id.invoice_code"", 
                    reason : ""$_id.reason"", 
                    document_date: ""$_id.document_date"",
                    infos: ""infos""
                }";
            var projection_store = @"
                {
                    ""delivery"": 0,
                    ""infos.store.material.receiving"": 0,
                    ""infos.store.material.material_group"": 0,
                    ""infos.store.material.material_subgroup"": 0
                }";
            var append_receivingmap = @"
                {
                    $addFields: {
                        ""receiving_map"": { $concat: [ ""$infos.receiving_code"", ""_"", ""$infos.code"" ] }
                    }
                }";
            var append_store = @"
                {
                    $addFields: {
                        ""infos.store.material"": ""$delivery"",
                        ""infos.store.material_group"": ""$delivery.material_group"",
                        ""infos.store.material_subgroup"": ""$delivery.material_subgroup"",
                        ""infos.store.inventory"": ""$delivery.inventory"",
                        ""infos.store.price"": ""$delivery.Price"",
                        ""infos.store.receiving"": ""$delivery.receiving"",
                        ""infos.store.last"": ""$Infos.quantity""
                    }
                }";
            return mappings
                .Unwind("infos", unwind)
                .AppendStage<BsonDocument>(BsonDocument.Parse(append_receivingmap))
                .Lookup("material_for_delivery_view", "receiving_map", "receiving_map", "delivery")
                .Unwind("delivery", unwind)
                .AppendStage<BsonDocument>(BsonDocument.Parse(append_store))
                .Project(BsonDocument.Parse(projection_store))
                .Group(BsonDocument.Parse(group))
                .Project(BsonDocument.Parse(projection))
                .Lookup("customer", "customer_code", "code", "customer")
                .Unwind("customer", unwind)
                .Lookup("customer", "other_customer_code", "code", "other_customer")
                .Unwind("other_customer", unwind)
                .Lookup("supplier", "supplier_code", "code", "supplier")
                .Unwind("supplier", unwind)
                .As<MaterialDeliveryVoucherView>().Match(context.GetPostExpression());
        }

        public async Task<MaterialDeliveryVoucherView> GetByDefCode(string code)
        {
            return await GetSingleByExpression(x => x.DefCode == code);
        }
    }
}
