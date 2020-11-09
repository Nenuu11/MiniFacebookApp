using aphrie.app.Extensions;
using aphrie.app.Models;
using aphrie.app.Settings;
using aphrie.BLL;
using aphrie.DBL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace aphrie.app.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JWT _jwt;
        private readonly UnitOfWork _unitOfWork;

        public UserService(
            UserManager<ApplicationUser> userManager, 
            IOptions<JWT> jwt,
            IHttpContextAccessor httpContextAccessor,
            UnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse> AddFriendAsync(string userName)
        {
            var friend = await _userManager.FindByEmailAsync(userName);
            if(friend == null)
            {
                return new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Data = "There is no user with this email. Please, enter another username."
                };
            }
            var loggedInUsername =  _httpContextAccessor.HttpContext.User.GetLoggedInUserEmail();
            if(loggedInUsername == userName)
            {
                return new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = "This username belongs to the logged in user. please, try a different one."
                };
            }
            var loggedInUser = await _userManager.FindByEmailAsync(loggedInUsername);

            var newFriend = new Friend
            {
                FriendId = friend.Id.ToString(),
                UserId = loggedInUser.Id.ToString(),
            };
            //check on the friend list
            var allFriends = _unitOfWork.FriendManager.GetAll().Where(f => f.UserId == loggedInUser.Id).ToList();
            if (loggedInUser.Friends.Where(u => u.FriendId == newFriend.FriendId).ToList().Count != 0)
            {
                return new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Data = $"{userName} is already your friend"
                };
            }

            await _unitOfWork.FriendManager.AddAsync(newFriend);
            
            return new ApiResponse
            {
                Data = $"Your friend {userName} has been added",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public async Task<ApiResponse> LoginAsync(LoginModel model)
        {
            var response = new ApiResponse();
            var authenticationModel = new AuthenticationModal();
            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user == null)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Data = $"Account Not Found.";
                return response;
            }

            if(await _userManager.CheckPasswordAsync(user, model.Password))
            {
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = user.Email;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Data = authenticationModel.Token;

                return response;
            }

            response.StatusCode = (int)HttpStatusCode.BadRequest;
            response.Data = $"Incorrect Credentials for user {user.Email}";
            return response;
        }

        public async Task<ApiResponse> RegisterAsync(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.Phone
            };

            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);

            if(userWithSameEmail != null)
            {
                return new ApiResponse
                {
                    Data = "There is an existing account with this Email.",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                return new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }

            return new ApiResponse
            {
                Data = "Error while creating the account.",
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
            }.Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer:_jwt.Issuer,
                audience:_jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

    }
}
