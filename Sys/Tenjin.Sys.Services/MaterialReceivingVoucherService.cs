using MongoDB.Bson;
using MongoDB.Driver;
using Tenjin.Reflections;
using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Services
{
    public class MaterialReceivingVoucherService : BaseService<MaterialReceivingVoucher, MaterialReceivingVoucherView>, IMaterialReceivingVoucherService
    {
        private readonly ISysContext _context;
        public MaterialReceivingVoucherService(ISysContext context) : base(context.MaterialReceivingVoucherRepository)
        {
            _context = context;
        }

        protected override IAggregateFluent<MaterialReceivingVoucherView> ConvertToViewAggreagate(IAggregateFluent<MaterialReceivingVoucher> mappings, IExpressionContext<MaterialReceivingVoucher, MaterialReceivingVoucherView> context)
        {
            var unwind = new AggregateUnwindOptions<MaterialReceivingVoucher> { PreserveNullAndEmptyArrays = true };
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
                        delivery_code: ""$delivery_code"",
                        supplier_code: ""$supplier_code"",
                        category : ""$category"", 
                        description: ""$description"",
                        document_date : ""$document_date""
                    },
                    infos: { ""$addToSet"": ""$infos"" }
                }";
            var projection = @"
                {
                   _id : ""$_id._id"", 
                    code : ""$_id.Code"", 
                    def_code : ""$_id.DefCode"",
                    name : ""$_id.name"",
                    value_to_search : ""$_id.value_to_search"", 
                    is_published : ""$_id.is_published"", 
                    created_date : ""$_id.created_date"", 
                    last_modified : ""$_id.last_modified"",
                    delivery_code: ""$_id.delivery_code"",
                    supplier_code: ""$_id.supplier_code"",
                    category : ""$_id.category"", 
                    description: ""$_id.description"",
                    document_date : ""$_id.document_date""
                    infos: ""infos""
                }";

            return mappings
                .Unwind("infos", unwind)
                .Lookup("material", "infos.code", "code", "infos.store.material")
                .Unwind("infos.store.material", unwind)
                .Lookup("material_group", "infos.store.material.material_group_code", "code", "infos.store.material_group")
                .Unwind("infos.store.material_group", unwind)
                .Lookup("material_subgroup", "infos.store.material.material_subgroup_code", "code", "infos.store.material_subgroup")
                .Unwind("infos.store.material_subgroup", unwind)
                .Group(BsonDocument.Parse(group))
                .Project(BsonDocument.Parse(projection))
                .Lookup("supplier", "supplier_code", "code", "supplier")
                .Unwind("supplier", unwind)
                .As<MaterialReceivingVoucherView>().Match(context.GetPostExpression());
        }

    }
}
