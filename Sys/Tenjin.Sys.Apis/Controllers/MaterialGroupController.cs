using Microsoft.AspNetCore.Mvc;
using Tenjin.Apis.Controllers;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Apis.Controllers
{
    [ApiController]
    [Route("api/material-group")]
    public class MaterialGroupController : BaseController<MaterialGroup>
    {
        public MaterialGroupController(IMaterialGroupService service) : base(service) { }
    }
}
