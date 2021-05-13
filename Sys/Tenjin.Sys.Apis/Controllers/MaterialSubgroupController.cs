using Microsoft.AspNetCore.Mvc;
using Tenjin.Apis.Controllers;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Apis.Controllers
{
    [ApiController]
    [Route("api/material-subgroup")]
    public class MaterialSubgroupController : BaseController<MaterialSubgroup, MaterialSubgroupView>
    {
        public MaterialSubgroupController(IMaterialSubgroupService service) : base(service) { }
    }
}
