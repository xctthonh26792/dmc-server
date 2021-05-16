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
    [Route("api/warehouse-inventory")]
    public class WarehouseInventoryController : BaseController<WarehouseInventory, WarehouseInventoryView>
    {
        private readonly IWarehouseInventoryService _service;
        public WarehouseInventoryController(IWarehouseInventoryService service) : base(service)
        {
            _service = service;
        }

        [HttpGet("{code}/{page:int}/{quantity:int}")]
        public  async Task<IActionResult> GetPageByCode(string code, int page, int quantity)
        {
            return Ok(await _service.GetPageByWarehouseCode(code, page, quantity));
        }
    }
}
