using Microsoft.AspNetCore.Mvc;
using Tenjin.Apis.Controllers;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Apis.Controllers
{
    [ApiController]
    [Route("api/job-position")]
    public class JobPositionController : BaseController<JobPosition, JobPositionView>
    {
        public JobPositionController(IJobPositionService service) : base(service)
        {

        }
    }
}
