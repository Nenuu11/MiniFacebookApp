using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aphrie.app.Localize;
using aphrie.app.Services;
using LoggerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace aphrie.app.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILoggerManager _logger;
        public UserController(IUserService userService, ILoggerManager logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("AddFriend")]
        public async Task<IActionResult> AddFriendAsync([FromBody] string username)
        {
            _logger.LogInfo("Adding friend...");
            var result = await _userService.AddFriendAsync(username);

            _logger.LogInfo($"Returning --> {result.Data}.");
            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
