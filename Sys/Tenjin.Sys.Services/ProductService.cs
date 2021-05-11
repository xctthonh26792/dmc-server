using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
    public class ProductService : BaseService<Product, ProductView>, IProductService
    {
        private readonly ISysContext _context;
        public ProductService(ISysContext context) : base(context.ProductRepository)
        {
            _context = context;
        }

        protected override IAggregateFluent<ProductView> ConvertToViewAggreagate(IAggregateFluent<Product> mappings, IExpressionContext<Product, ProductView> context)
        {
            var unwind = new AggregateUnwindOptions<ProductView> { PreserveNullAndEmptyArrays = true };
            return mappings.Lookup("product_group", "product_group_code", "code", "product_group").Unwind("product_group", unwind)
                .Lookup("product_subgroup", "product_subgroup_code", "code", "product_subgroup").Unwind("product_subgroup", unwind)
                .As<ProductView>().Match(context.GetPostExpression());
        }

        protected override Task InitializeInsertModel(Product entity)
        {
            if (entity == null)
            {
                return null;
            }
            entity.SortName = GetSortName(entity.Name);
            return base.InitializeInsertModel(entity);
        }

        protected override Task InitializeReplaceModel(Product entity)
        {
            if (entity == null)
            {
                return null;
            }
            entity.SortName = GetSortName(entity.Name);
            return base.InitializeReplaceModel(entity);
        }

        private string GetSortName(string name)
        {
            var arrays = new List<string>();
            Regex regex = new Regex(@"(\d+)");
            var splits = regex.Split(name);
            char last = char.MinValue;
            foreach (var split in splits)
            {
                if (!split.Any(x => char.IsNumber(x)))
                {
                    last = split.LastOrDefault();
                    arrays.Add(split);
                    continue;
                }
                arrays.Add(last == '.' || last == ',' ? split.PadRight(8, '0') : split.PadLeft(8, '0'));
            }
            return string.Join("", arrays).ViToEn(false);
        }
    }
}