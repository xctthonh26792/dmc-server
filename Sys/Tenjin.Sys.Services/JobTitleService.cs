using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Services
{
    public class JobTitleService : BaseService<JobTitle>, IJobTitleService
    {
        public JobTitleService(ISysContext context) : base(context.JobTitleRepository)
        {
        }
    }
}
