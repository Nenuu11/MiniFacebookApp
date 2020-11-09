using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aphrie.app.Models;
using aphrie.app.Services;
using aphrie.DBL.Entities;
using LoggerService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace aphrie.app.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILoggerManager _logger;

        public AuthenticationController(IUserService userService, ILoggerManager logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            _logger.LogInfo("Registering Account....");
            var result = await _userService.RegisterAsync(model);

            _logger.LogInfo("Account Registered Successfully.");
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            _logger.LogInfo("Login...");
            var result = await _userService.LoginAsync(model);

            _logger.LogInfo("Successfully Logged in");
            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
