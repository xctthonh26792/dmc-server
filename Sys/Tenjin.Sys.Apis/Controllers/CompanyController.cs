using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tenjin.Apis.Controllers;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Apis.Controllers
{
    [ApiController]
    [Route("api/company")]
    public class CompanyController : TenjinController
    {
        private readonly ICompanyService _service;
        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var company = await _service.GetCompanyProfile();
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Company company)
        {
            await _service.UpdateCompanyProfile(company);
            return Ok();
        }
    }
}
