using MongoDB.Driver;
using System.Threading.Tasks;
using Tenjin.Models.Cores;
using Tenjin.Services.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;

namespace Tenjin.Sys.Services.Interfaces
{
    public interface IWarehouseInventoryService : IBaseService<WarehouseInventory, WarehouseInventoryView>
    {
        Task<FetchResult<WarehouseInventoryView>> GetPageByWarehouseCode(string code, int page, int quantity = 10, SortDefinition<WarehouseInventory> sort = null, ProjectionDefinition<WarehouseInventory> projection = null);
       

    }
}
