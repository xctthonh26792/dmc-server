using Microsoft.AspNetCore.Mvc;
using Tenjin.Apis.Controllers;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Apis.Controllers
{
    [ApiController]
    [Route("api/job-title")]
    public class JobTitleController : BaseController<JobTitle>
    {
        public JobTitleController(IJobTitleService service) : base(service) { }
    }
}
