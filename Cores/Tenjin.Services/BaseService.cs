using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tenjin.Contracts.Interfaces;
using Tenjin.Helpers;
using Tenjin.Models.Cores;
using Tenjin.Models.Entities;
using Tenjin.Models.Interfaces;
using Tenjin.Serializations;
using Tenjin.Services.Interfaces;

namespace Tenjin.Services
{
    public class BaseService
    {
        protected bool IsObjectId(string code)
        {
            return ObjectId.TryParse(code, out _);
        }

        protected Expression<Func<T, bool>> GetFilterExpression<T>(string code)
            where T : IEntity
        {
            Expression<Func<T, bool>> filter = x => x.Id.Equals(code);
            return filter;
        }

        protected void PrepareInsertModel<T>(T entity)
            where T : IEntity
        {
            entity.CreatedDate = DateTime.Now;
            entity.LastModified = DateTime.Now;
        }

        protected void PrepareReplaceModel<T>(T entity)
            where T : IEntity
        {
            entity.LastModified = DateTime.Now;
        }
    }

    public abstract class BaseService<T> : BaseService, IBaseService<T>
        where T : IEntity
    {
        private readonly IContext _context;
        private readonly IRepository<T> _repository;

        protected BaseService(IContext context, IRepository<T> repository)
        {
            _context = context;
            _repository = repository;
        }

        protected abstract string Tag();

        protected IRepository<T> GetRepository()
        {
            return _repository;
        }

        public virtual async Task Add(T entity)
        {
            if (entity == null) return;
            PrepareInsertModel(entity);
            await InitializeInsertModel(entity);
            await _repository.Add(entity);
        }

        public virtual async Task AddMany(IEnumerable<T> entities)
        {
            if (entities == null) return;
            foreach (var entity in entities)
            {
                PrepareInsertModel(entity);
                await InitializeInsertModel(entity);
            }
            await _repository.AddMany(entities);
        }

        public virtual async Task<long> Count(string code)
        {
            return await Count(GetFilterExpression<T>(code));
        }

        public virtual async Task<long> Count(Expression<Func<T, bool>> filter)
        {
            return await _repository.Count(filter);
        }

        public virtual async Task<long> Count(FilterDefinition<T> filter)
        {
            return await _repository.Count(filter);
        }

        public virtual async Task Delete(string code)
        {
            await _repository.Delete(GetFilterExpression<T>(code));
        }

        public IAggregateFluent<T> GetAggregate()
        {
            return _repository.GetAggregate();
        }

        public virtual async Task<T> GetByCode(string code)
        {
            if (TenjinUtils.IsStringEmpty(code)) return default;
            return await _repository.GetSingleByExpression(GetFilterExpression<T>(code));
        }

        public virtual async Task<IEnumerable<T>> GetByExpression(Expression<Func<T, bool>> filter, SortDefinition<T> sort = null, ProjectionDefinition<T> projection = null)
        {
            return await _repository.GetByExpression(filter, sort, projection);
        }

        public virtual async Task<IEnumerable<T>> GetByExpression(FilterDefinition<T> filter, SortDefinition<T> sort = null, ProjectionDefinition<T> projection = null)
        {
            return await _repository.GetByExpression(filter, sort, projection);
        }

        public virtual async Task<FetchResult<T>> GetPageByExpression(Expression<Func<T, bool>> filter, int page, int quantity = 10, SortDefinition<T> sort = null, ProjectionDefinition<T> projection = null)
        {
            return new FetchResult<T>
            {
                Count = await _repository.Count(filter),
                Models = await _repository.GetPageByExpression(page, quantity > 0 ? quantity : 10, filter, sort, projection)
            };
        }

        public virtual async Task<FetchResult<T>> GetPageByExpression(FilterDefinition<T> filter, int page, int quantity = 10, SortDefinition<T> sort = null, ProjectionDefinition<T> projection = null)
        {
            return new FetchResult<T>
            {
                Count = await _repository.Count(filter),
                Models = await _repository.GetPageByExpression(page, quantity > 0 ? quantity : 10, filter, sort, projection)
            };
        }

        public virtual async Task<T> GetSingleByExpression(Expression<Func<T, bool>> filter, SortDefinition<T> sort = null, ProjectionDefinition<T> projection = null, int index = 1)
        {
            return await _repository.GetSingleByExpression(filter, sort, projection, index);
        }

        public virtual async Task<T> GetSingleByExpression(FilterDefinition<T> filter, SortDefinition<T> sort = null, ProjectionDefinition<T> projection = null, int index = 1)
        {
            return await _repository.GetSingleByExpression(filter, sort, projection, index);
        }

        public virtual async Task Replace(T entity)
        {
            if (entity == null) return;
            PrepareReplaceModel(entity);
            await InitializeReplaceModel(entity);
            await _repository.Replace(entity);
        }

        public async Task Published(string code)
        {
            await _repository.UpdateOne(GetFilterExpression<T>(code), Builders<T>.Update.Set(x => x.IsPublished, true));
        }

        public async Task Unpublished(string code)
        {
            await _repository.UpdateOne(GetFilterExpression<T>(code), Builders<T>.Update.Set(x => x.IsPublished, false));
        }

        public virtual async Task CreateIndexes()
        {
            var manager = GetRepository().GetCollection().Indexes;
            {
                if (TenjinMongoUtils.GenerateIndexes<T>(out var definitions))
                {
                    await manager.CreateOneAsync(CreateIndexModel($"{typeof(T).Name.ToRegular()}_auto", definitions));
                }
            }
            {
                if (TenjinMongoUtils.GenerateTextIndexes<T>(out var definitions))
                {
                    await manager.CreateOneAsync(CreateIndexModel($"{typeof(T).Name.ToRegular()}_text", definitions));
                }
            }
            static CreateIndexModel<T> CreateIndexModel(string name, IndexKeysDefinition<T> definition)
            {
                var options = new CreateIndexOptions { Name = name };
                return new CreateIndexModel<T>(definition, options);
            }
        }

        protected virtual async Task InitializeInsertModel(T entity)
        {
            entity.Code = !string.IsNullOrEmpty(entity.Code) ? entity.Code : await GenerateCode();
        }

        protected virtual async Task InitializeReplaceModel(T entity)
        {
            await Task.Yield();
        }

        protected virtual string PrefixCode()
        {
            return DateTime.Now.ToString("yyyyMM");
        }

        protected virtual string Coding(string prefix, CodeGenerate model)
        {
            return $"{prefix}{model.Count:D5}{TenjinUtils.RandomString(5).ToUpper()}";
        }

        public virtual async Task<string> GenerateCode()
        {
            var prefix = PrefixCode();
            var code = $"{Tag()}-{prefix}";
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
            return Coding(prefix, model);
        }
    }

    public abstract class BaseService<T, TV> : BaseService, IBaseService<T, TV>
        where T : BaseEntity
        where TV : T
    {
        private readonly IContext _context;
        private readonly IRepository<T> _repository;

        protected BaseService(IContext context, IRepository<T> repository)
        {
            _context = context;
            _repository = repository;
        }

        protected abstract string Tag();

        protected IRepository<T> GetRepository()
        {
            return _repository;
        }

        public virtual async Task Add(T entity)
        {
            if (entity == null) return;
            PrepareInsertModel(entity);
            await InitializeInsertModel(entity);
            await _repository.Add(entity);
        }

        public virtual async Task AddMany(IEnumerable<T> entities)
        {
            if (entities == null) return;
            foreach (var entity in entities)
            {
                PrepareInsertModel(entity);
                await InitializeInsertModel(entity);
            }
            await _repository.AddMany(entities);
        }

        public virtual async Task<long> Count(string code)
        {
            return await _repository.Count(GetFilterExpression<T>(code));
        }

        public virtual async Task<long> Count(Expression<Func<T, bool>> filter)
        {
            var aggregate = CreateViewAggregate(filter);
            var counter = await aggregate.Count().FirstOrDefaultAsync();
            return counter?.Count ?? 0;
        }

        public virtual async Task<long> Count(FilterDefinition<T> filter)
        {
            var aggregate = CreateViewAggregate(filter);
            var counter = await aggregate.Count().FirstOrDefaultAsync();
            return counter?.Count ?? 0;
        }

        public virtual async Task Delete(string code)
        {
            await _repository.Delete(GetFilterExpression<T>(code));
        }

        public IAggregateFluent<T> GetAggregate()
        {
            return _repository.GetAggregate();
        }

        public virtual async Task<TV> GetByCode(string code)
        {
            if (TenjinUtils.IsStringEmpty(code)) return default;
            var aggregate = CreateViewAggregate(GetFilterExpression<T>(code));
            return await aggregate.FirstOrDefaultAsync();
        }

        public virtual async Task<TV> GetSingleByExpression(Expression<Func<T, bool>> filter, SortDefinition<T> sort = null, ProjectionDefinition<T> projection = null, int index = 1)
        {
            var aggregate = CreateViewAggregate(filter, sort, projection).Skip(index - 1);
            return await aggregate.FirstOrDefaultAsync();
        }

        public virtual async Task<TV> GetSingleByExpression(FilterDefinition<T> filter, SortDefinition<T> sort = null, ProjectionDefinition<T> projection = null, int index = 1)
        {
            var aggregate = CreateViewAggregate(filter, sort, projection).Skip(index - 1);
            return await aggregate.FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TV>> GetByExpression(Expression<Func<T, bool>> filter, SortDefinition<T> sort = null, ProjectionDefinition<T> projection = null)
        {
            var aggregate = CreateViewAggregate(filter, sort, projection);
            return await aggregate.ToListAsync();
        }

        public virtual async Task<IEnumerable<TV>> GetByExpression(FilterDefinition<T> filter, SortDefinition<T> sort = null, ProjectionDefinition<T> projection = null)
        {
            var aggregate = CreateViewAggregate(filter, sort, projection);
            return await aggregate.ToListAsync();
        }

        public virtual async Task<FetchResult<TV>> GetPageByExpression(Expression<Func<T, bool>> filter, int page, int quantity = 10, SortDefinition<T> sort = null, ProjectionDefinition<T> projection = null)
        {
            var aggregate = CreateViewAggregate(filter, sort, projection);
            var counter = await aggregate.Count().FirstOrDefaultAsync();
            return new FetchResult<TV>
            {
                Count = counter?.Count ?? 0,
                Models = await aggregate.Skip((Math.Max(0, page - 1) * quantity)).Limit(quantity > 0 ? quantity : 10).ToListAsync()
            };
        }

        public virtual async Task<FetchResult<TV>> GetPageByExpression(FilterDefinition<T> filter, int page, int quantity = 10, SortDefinition<T> sort = null, ProjectionDefinition<T> projection = null)
        {
            var aggregate = CreateViewAggregate(filter, sort, projection);
            var counter = await aggregate.Count().FirstOrDefaultAsync();
            return new FetchResult<TV>
            {
                Count = counter?.Count ?? 0,
                Models = await aggregate.Skip((Math.Max(0, page - 1) * quantity)).Limit(quantity > 0 ? quantity : 10).ToListAsync()
            };
        }

        public virtual async Task Replace(T entity)
        {
            if (entity == null) return;
            PrepareReplaceModel(entity);
            await InitializeReplaceModel(entity);
            await _repository.Replace(entity);
        }

        public async Task Published(string code)
        {
            await _repository.UpdateOne(GetFilterExpression<T>(code), Builders<T>.Update.Set(x => x.IsPublished, true));
        }

        public async Task Unpublished(string code)
        {
            await _repository.UpdateOne(GetFilterExpression<T>(code), Builders<T>.Update.Set(x => x.IsPublished, false));
        }

        public virtual async Task CreateIndexes()
        {
            var manager = GetRepository().GetCollection().Indexes;
            {
                if (TenjinMongoUtils.GenerateIndexes<T>(out var definitions))
                {
                    await manager.CreateOneAsync(CreateIndexModel($"{typeof(T).Name.ToRegular()}_auto", definitions));
                }
            }
            {
                if (TenjinMongoUtils.GenerateTextIndexes<T>(out var definitions))
                {
                    await manager.CreateOneAsync(CreateIndexModel($"{typeof(T).Name.ToRegular()}_text", definitions));
                }
            }
            static CreateIndexModel<T> CreateIndexModel(string name, IndexKeysDefinition<T> definition)
            {
                var options = new CreateIndexOptions { Name = name };
                return new CreateIndexModel<T>(definition, options);
            }
        }

        protected virtual async Task InitializeInsertModel(T entity)
        {
            entity.Code = !string.IsNullOrEmpty(entity.Code) ? entity.Code : await GenerateCode();
        }

        protected virtual async Task InitializeReplaceModel(T entity)
        {
            await Task.Yield();
        }

        protected IAggregateFluent<TV> CreateViewAggregate(FilterDefinition<T> filter = null, SortDefinition<T> sort = null, ProjectionDefinition<T> projection = null)
        {
            var aggregate = GetAggregate();
            if (filter != null)
            {
                aggregate = aggregate.Match(filter);
            }
            if (sort != null)
            {
                aggregate = aggregate.Sort(sort);
            }
            if (projection != null)
            {
                aggregate = aggregate.Project(projection).As<T>();
            }
            return ConvertToViewAggreagate(aggregate);
        }

        protected virtual IAggregateFluent<TV> ConvertToViewAggreagate(IAggregateFluent<T> mappings)
        {
            return mappings.As<TV>();
        }

        protected virtual string PrefixCode()
        {
            return DateTime.Now.ToString("yyyyMM");
        }

        protected virtual string Coding(string prefix, CodeGenerate model)
        {
            return $"{prefix}{model.Count:D5}{TenjinUtils.RandomString(5).ToUpper()}";
        }

        public virtual async Task<string> GenerateCode()
        {
            var prefix = PrefixCode();
            var code = string.IsNullOrEmpty(prefix) ? $"{Tag()}" : $"{Tag()}-{prefix}";
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
            return Coding(prefix, model);
        }
    }
}
