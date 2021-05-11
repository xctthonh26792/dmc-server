using Tenjin.Services.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;

namespace Tenjin.Sys.Services.Interfaces
{
    public interface ICustomerService : IBaseService<Customer, CustomerView>
    {
    }
}
