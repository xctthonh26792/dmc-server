using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tenjin.Apis.Controllers;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Apis.Controllers
{
    [ApiController]
    [Route("api/material-group-type")]
    public class MaterialGroupTypeController : BaseController<MaterialGroupType, MaterialGroupTypeView>
    {
        private readonly IMaterialGroupTypeService _service;
        public MaterialGroupTypeController(IMaterialGroupTypeService service) : base(service)
        {
            _service = service;
        }
    }
}
