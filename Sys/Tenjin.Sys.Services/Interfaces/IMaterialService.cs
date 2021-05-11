using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tenjin.Models.Cores;
using Tenjin.Services.Interfaces;
using Tenjin.Sys.Models.Cores;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;

namespace Tenjin.Sys.Services.Interfaces
{
    public interface IMaterialService : IBaseService<Material, MaterialView>
    {
        Task EmbedBarcode(string code, string barcode);
        Task<FetchResult<MaterialForReceiving>> GetReceivingByPageAndQuantity(Expression<Func<MaterialForReceiving, bool>> expression, int page,
            int quantity = 10, string sortByField = "CreatedDate", SortDefinition<MaterialForReceiving> sort = null,
            string query = "");
        Task<FetchResult<MaterialForDelivery>> GetDeliveryByPageAndQuantity(Expression<Func<MaterialForDelivery, bool>> expression, int page,
            int quantity = 10, string sortByField = "SortName", SortDefinition<MaterialForDelivery> sort = null,
            string query = "");
        Task<FetchResult<MaterialForInventory>> GetInventoryByPageAndQuantity(Expression<Func<MaterialForInventory, bool>> expression, int page, int quantity = 10,
            string sortByField = "CreatedDate", SortDefinition<MaterialForInventory> sort = null, string searchValue = "");

        Task<MaterialView> GetByFullCode(string fullcode);
    }
}
