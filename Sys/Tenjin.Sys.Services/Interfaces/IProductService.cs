using Tenjin.Sys.Models.Views;
using Tenjin.Services.Interfaces;
using Tenjin.Sys.Models.Entities;


namespace Tenjin.Sys.Services.Interfaces
{
    public interface IProductService : IBaseService<Product, ProductView>
    {
    }
}
