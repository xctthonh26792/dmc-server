using Microsoft.AspNetCore.Mvc;
using Tenjin.Apis.Controllers;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Apis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : BaseController<Warehouse>
    {
        public WarehouseController(IWarehouseService service) : base(service) { }
    }
}
