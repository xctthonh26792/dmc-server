﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tenjin.Helpers;
using Tenjin.Models.Cores;
using Tenjin.Reflections;
using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Cores;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Services
{
    public class MaterialService : BaseService<Material, MaterialView>, IMaterialService
    {
        private readonly ISysContext _context;
        public MaterialService(ISysContext context) : base(context.MaterialRepository)
        {
            _context = context;
        }

        protected override IAggregateFluent<MaterialView> ConvertToViewAggreagate(IAggregateFluent<Material> mappings, IExpressionContext<Material, MaterialView> context)
        {
            var unwind = new AggregateUnwindOptions<MaterialView> { PreserveNullAndEmptyArrays = true };
            return mappings
                .Lookup("material_group", "material_group_code", "code", "material_group")
                .Unwind("material_group", unwind)
                .Lookup("material_subgroup", "material_subgroup_code", "code", "material_subgroup")
                .Unwind("material_subgroup", unwind)
                .Lookup("unit", "unit_code", "code", "unit")
                .Unwind("unit", unwind)
                .As<MaterialView>().Match(context.GetPostExpression());
        }


        public async Task<FetchResult<MaterialForDelivery>> GetDeliveryByPageAndQuantity(Expression<Func<MaterialForDelivery, bool>> expression, int page,
            int quantity = 10, string sortByField = "SortName", SortDefinition<MaterialForDelivery> sort = null,
            string query = "")
        {
            query = query.ViToEn();
            expression = expression ?? (x => true);
            if (!string.IsNullOrEmpty(query))
            {
                var filter = GetDeliverySearchExpression(query);
                if (filter != null)
                {
                    expression = expression.And(filter);
                }
            }
            var collection = _repository.GetMongoCollection<MaterialForDelivery>("MaterialForDeliveryView");
            var aggregate = collection.Aggregate().Match(expression);
            var count = await aggregate.Count().FirstOrDefaultAsync();
            return new FetchResult<MaterialForDelivery>
            {
                Count = count?.Count ?? 0,
                Models = await aggregate.Sort(sort).Skip((page - 1) * quantity).Limit(quantity).ToListAsync()
            };
        }

        private Expression<Func<MaterialForDelivery, bool>> GetDeliverySearchExpression(string query)
        {
            var splits = query.Split(' ', ',', ';', ':', '|', '/', '\\');
            if (!splits.Any())
            {
                return x => true;
            }
            Expression<Func<MaterialForDelivery, bool>> expression = x => true;
            foreach (var split in splits)
            {
                expression = expression.And(x => x.ValueToSearch.Contains(split));
            }
            return expression;
        }

        public async Task<FetchResult<MaterialForReceiving>> GetReceivingByPageAndQuantity(Expression<Func<MaterialForReceiving, bool>> expression, int page,
            int quantity = 10, string sortByField = "SortName", SortDefinition<MaterialForReceiving> sort = null,
            string query = "")
        {

            query = query.ViToEn();
            expression = expression ?? (x => true);
            if (!string.IsNullOrEmpty(query))
            {
                var filter = GetReceivingSearchExpression(query);
                if (filter != null)
                {
                    expression = expression.And(filter);
                }
            }
            var collection = _repository.GetMongoCollection<MaterialForReceiving>("MaterialForReceivingView");
            var aggregate = collection.Aggregate().Match(expression);
            var count = await aggregate.Count().FirstOrDefaultAsync();
            return new FetchResult<MaterialForReceiving>
            {
                Count = count?.Count ?? 0,
                Models = await aggregate.Sort(sort).Skip((page - 1) * quantity).Limit(quantity).ToListAsync()
            };
        }

        private Expression<Func<MaterialForReceiving, bool>> GetReceivingSearchExpression(string query)
        {
            var splits = query.Split(' ', ',', ';', ':', '|', '/', '\\');
            if (!splits.Any())
            {
                return x => true;
            }
            Expression<Func<MaterialForReceiving, bool>> expression = x => true;
            foreach (var split in splits)
            {
                expression = expression.And(x => x.ValueToSearch.Contains(split));
            }
            return expression;
        }


        public async Task EmbedBarcode(string code, string barcode)
        {
            var updater = Builders<MaterialBarcode>.Update.Set(x => x.MaterialCode, code).Set(x => x.LastModified, DateTime.Now);
            await _context.MaterialBarcodeRepository.UpdateOne(x => x.Barcode == barcode, updater, true);
        }

        protected override Task InitializeInsertModel(Material entity)
        {
            if (entity == null)
            {
                return null;
            }
            entity.SortName = GetSortName(entity.Name);
            return base.InitializeInsertModel(entity);
        }

        protected override Task InitializeReplaceModel(Material entity)
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


        public async Task<FetchResult<MaterialForInventory>> GetInventoryByPageAndQuantity(Expression<Func<MaterialForInventory, bool>> expression, int page, int quantity = 10,
            string sortByField = "CreatedDate", SortDefinition<MaterialForInventory> sort = null, string query = "")
        {
            query = query.ViToEn();
            expression = expression ?? (x => true);
            if (!string.IsNullOrEmpty(query))
            {
                var filter = GetInventorySearchExpression(query);
                if (filter != null)
                {
                    expression = expression.And(filter);
                }
            }
            var collection = _repository.GetMongoCollection<MaterialForInventory>("MaterialForInventoryView");
            var aggregate = collection.Aggregate().Match(expression);
            var count = await aggregate.Count().FirstOrDefaultAsync();
            return new FetchResult<MaterialForInventory>
            {
                Count = count?.Count ?? 0,
                Models = await aggregate.Sort(sort).Skip((page - 1) * quantity).Limit(quantity).ToListAsync()
            };
        }

        public Expression<Func<MaterialForInventory, bool>> GetInventorySearchExpression(string query)
        {
            var splits = query.Split(' ', ',', ';', ':', '|', '/', '\\');
            if (!splits.Any())
            {
                return x => true;
            }
            Expression<Func<MaterialForInventory, bool>> expression = x => true;
            foreach (var split in splits)
            {
                expression = expression.And(x => x.ValueToSearch.Contains(split));
            }
            return expression;
        }

        public async Task<MaterialView> GetByFullCode(string fullcode)
        {
            fullcode = fullcode.Replace("VT", ""); ;
            var splits = fullcode.Split('-');
            var groupcode = splits.ElementAtOrDefault(0);
            var subgroupcode = splits.ElementAtOrDefault(1);
            var materialcode = splits.ElementAtOrDefault(2);
            if (string.IsNullOrEmpty(groupcode) || string.IsNullOrEmpty(subgroupcode) || string.IsNullOrEmpty(materialcode))
            {
                return null;
            }
            var group = await _context.MaterialGroupRepository.GetSingleByExpression(x => x.IsPublished == true && x.DefCode == groupcode);
            if (group == null)
            {
                return null;
            }
            var subgroup = await _context.MaterialSubgroupRepository.GetSingleByExpression(x => x.IsPublished == true && x.DefCode == subgroupcode);
            if (subgroup == null)
            {
                return null;
            }
            return await GetSingleByExpression(x => x.MaterialGroupCode == group.Code && x.MaterialSubgroupCode == subgroup.Code
            && x.DefCode == materialcode);
        }
    }
}