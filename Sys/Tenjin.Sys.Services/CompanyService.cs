using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Services
{
    public class CompanyService : BaseService<Company>, ICompanyService
    {
        private readonly ISysContext _context;
        public CompanyService(ISysContext context) : base(context.CompanyRepository)
        {
            _context = context;
        }

        public async Task<Company> GetCompanyProfile()
        {
            var company = await GetSingleByExpression(x => x.Code == "0");
            if (company == null)
            {
                return new Company
                {
                    Code = "0",
                    DefCode = "0",
                    IsPublished = true
                };
            }
            return company;
        }

        public async Task UpdateCompanyProfile(Company company)
        {
            Expression<Func<Company, bool>> filter = x => x.Code == "0";
            var update = Builders<Company>.Update
                .SetOnInsert(x => x.Code, "0")
                .SetOnInsert(x => x.DefCode, "0")
                .SetOnInsert(x => x.IsPublished, true)
                .Set(x => x.Name, company.Name)
                .Set(x => x.Description, company.Description)
                .Set(x => x.Address, company.Address)
                .Set(x => x.Email, company.Email)
                .Set(x => x.Phone, company.Phone)
                .Set(x => x.Fax, company.Fax)
                .Set(x => x.TaxCode, company.TaxCode)
                .Set(x => x.Representative, company.Representative);
            await _context.CompanyRepository.UpdateOne(filter, update, true);
        }
    }
}
