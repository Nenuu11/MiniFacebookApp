using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using aphrie.app.Localize;
using aphrie.app.Models;
using aphrie.app.Services;
using aphrie.BLL;
using aphrie.DBL.Entities;
using LoggerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aphrie.app.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostServie _postService;
        private readonly ILoggerManager _logger;

        public PostController(IPostServie postService, ILoggerManager logger)
        {
            _postService = postService;
            _logger = logger;
        }


        [HttpPost("AddPost")]
        public async Task<IActionResult> AddPostAsync([FromBody] PostModel post  /*, [FromHeader(Name = "ar-EG")] string lang*/)
        {
            _logger.LogInfo("Adding Post to the database....");
            var result = await _postService.AddPostAsync(post);

            _logger.LogInfo($"Returning --> {result.Data}.");
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("AllPosts")]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            _logger.LogInfo("Fetching all posts from database..");
            var result = await _postService.GetAllPostsAsync();

            _logger.LogInfo($"Returning --> posts has been returned.");

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("DeletePost")]
        public async Task<IActionResult> DeletePostAsync([FromBody] string postId)
        {
            _logger.LogInfo("Deleting post....");
            var result = await _postService.DeletePostAsync(postId);

            _logger.LogInfo($"Returning --> {result.Data}.");
            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
