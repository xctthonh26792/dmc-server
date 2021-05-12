using Microsoft.AspNetCore.Mvc;
using Tenjin.Apis.Controllers;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Apis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : BaseController<Department, DepartmentView>
    {
        public DepartmentController(IDepartmentService service) : base(service)
        {
            
        }        
    }
}
