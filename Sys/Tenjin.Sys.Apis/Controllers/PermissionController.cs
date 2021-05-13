using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tenjin.Apis.Controllers;
using Tenjin.Sys.Models.Cores;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Apis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : TenjinController
    {
        private readonly IUserService _userService;
        public PermissionController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("{code}")]
        public async Task<IActionResult> Post(string code, [FromBody] IEnumerable<Permission> values)
        {
            if (values != null)
            {
                var user = await _userService.GetByCode(code);
                if (user == null)
                {
                    return BadRequest();
                }
                var content = JsonConvert.SerializeObject(values);
                user.Permissions = content;
                // don't update password
                user.Password = string.Empty;
                await _userService.Replace(user);
            }
            return Ok();
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code)
        {
            var user = await _userService.GetByCode(code);
            return Ok(GetPermissions(user?.Permissions));
        }

        private IEnumerable<Permission> GetPermissions(string content)
        {
            try
            {
                return JsonConvert.DeserializeObject<IEnumerable<Permission>>(content);
            }
            catch (Exception)
            {
                return new List<Permission>();
            }
        }
    }
}
