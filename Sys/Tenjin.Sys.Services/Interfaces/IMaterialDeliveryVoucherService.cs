using System.Threading.Tasks;
using Tenjin.Services.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;

namespace Tenjin.Sys.Services.Interfaces
{
    public interface IMaterialDeliveryVoucherService : IBaseService<MaterialDeliveryVoucher, MaterialDeliveryVoucherView>
    {
        Task<MaterialDeliveryVoucherView> GetByDefCode(string code);
    }
}
