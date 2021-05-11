using System.Threading.Tasks;
using Tenjin.Services.Interfaces;
using Tenjin.Sys.Models.Entities;

namespace Tenjin.Sys.Services.Interfaces
{
    public interface ICompanyService : IBaseService<Company>
    {
        Task<Company> GetCompanyProfile();
        Task UpdateCompanyProfile(Company company);
    }
}
