﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tenjin.Apis.Controllers;
using Tenjin.Helpers;
using Tenjin.Models;
using Tenjin.Sys.Models.Cores;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Apis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : BaseController<Employee, EmployeeView>
    {
        private readonly IEmployeeService _service;
        public EmployeeController(IEmployeeService service) : base(service)
        {
            _service = service;
        }
    }
}
