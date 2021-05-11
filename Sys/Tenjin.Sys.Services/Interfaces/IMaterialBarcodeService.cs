using System.Collections.Generic;
using System.Threading.Tasks;
using Tenjin.Services.Interfaces;
using Tenjin.Sys.Models.Entities;

namespace Tenjin.Sys.Services.Interfaces
{
    public interface IMaterialBarcodeService : IBaseService<MaterialBarcode>
    {
        Task<string> GetBarcode(string @ref);
        Task<IEnumerable<string>> GetBarcode(string @ref, string receipt, int count);
    }
}
