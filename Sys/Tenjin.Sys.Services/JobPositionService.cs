using Tenjin.Services;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;
using Tenjin.Sys.Contracts.Interfaces;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tenjin.Models.Enums;
using System.Linq.Expressions;
using System;
using Tenjin.Helpers;
using Tenjin.Reflections;

namespace Tenjin.Sys.Services
{
    public class JobPositionService : BaseService<JobPosition, JobPositionView>, IJobPositionService
    {
        private readonly ISysContext _context;
        public JobPositionService(ISysContext context) : base(context.JobPositionRepository)
        {
            _context = context;
        }

        protected override IAggregateFluent<JobPositionView> ConvertToViewAggreagate(IAggregateFluent<JobPosition> mappings, IExpressionContext<JobPosition, JobPositionView> context)
        {
            var unwind = new AggregateUnwindOptions<DepartmentView> { PreserveNullAndEmptyArrays = true };
            return mappings
                .Lookup("job_title", "job_title_code", "code", "job_title")
                .Unwind("job_title", unwind)
                .Lookup("department", "department_code", "code", "department")
                .Unwind("department", unwind)
                .As<JobPositionView>().Match(context.GetPostExpression());
        }
    }
}
